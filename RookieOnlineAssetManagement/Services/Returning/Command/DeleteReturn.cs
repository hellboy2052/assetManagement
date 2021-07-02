using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;

namespace RookieOnlineAssetManagement.Services.ReturningService.Command
{
    public class DeleteReturnCommand : IRequest<Result<Task>>
    {
        public string Id { get; set; }
    }
    public class DeleteReturnCommandHandler : IRequestHandler<DeleteReturnCommand, Result<Task>>
    {
        private readonly ApplicationDbContext _context;
        public DeleteReturnCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<Task>> Handle(DeleteReturnCommand command, CancellationToken cancellationToken)
        {
            var existReturning = await _context.Returning
                .Include(rt => rt.Assignment)
                .ThenInclude(asg => asg.Asset)
                .FirstOrDefaultAsync(rt => rt.ReturnId == command.Id);

            if (existReturning == null)
            {
                return Result<Task>.Failure($"ReturningId: {command.Id} is not exist");
            }

            if (existReturning.State != EnumsObject.Returning.WaitingForReturning.ToString())
            {
                return Result<Task>.Failure("Only Waiting for Returning is allow to delete");
            }

            try
            {
                existReturning.Assignment.State = EnumsObject.AssignmentState.Accepted.ToString();
                existReturning.Assignment.Asset.State = EnumsObject.State.Assigned.ToString();
                _context.Entry(existReturning).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return Result<Task>.Success(Task.CompletedTask);
            }
            catch (Exception e)
            {
                return Result<Task>.Failure(e.InnerException.Message);
            }

        }
    }
}
