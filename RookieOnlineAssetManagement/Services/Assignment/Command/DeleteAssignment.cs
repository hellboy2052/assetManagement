using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.AssignmentService.Command
{
    public class DeleteAssignmentCommand : IRequest<Result<Task>>
    {
        public string Id { get; set; }
    }
    public class DeleteAssignmentCommandHandler : IRequestHandler<DeleteAssignmentCommand, Result<Task>>
    {
        private readonly ApplicationDbContext _context;

        public DeleteAssignmentCommandHandler (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Task>> Handle (DeleteAssignmentCommand command, CancellationToken cancellationToken)
        {
            var existingAssignment = await _context.Assignments.Include(asg => asg.Asset).Include(asg => asg.Staff).FirstOrDefaultAsync(asg => asg.AssignmentId == command.Id);

            if (existingAssignment == null)
            {
                return Result<Task>.Failure($"Assignment Id : {command.Id} not exist");
            }

            if (existingAssignment.State != EnumsObject.AssignmentState.WaitingForAcceptance.ToString())
            {
                return Result<Task>.Failure($"Only waiting for acceptance assignment is allow to delete");
            }

            try
            {
                existingAssignment.Staff.IsAssigned = false;
                existingAssignment.Asset.IsAssigned = false;
                existingAssignment.Asset.State = EnumsObject.State.Available.ToString();
                _context.Assignments.Remove(existingAssignment);
                await _context.SaveChangesAsync();
                return Result<Task>.Success(Task.CompletedTask);
            }
            catch (Exception ex)
            {
                return Result<Task>.Failure(ex.InnerException.Message);
            }

        }
    }
}
