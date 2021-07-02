using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.ViewModels.Assignment;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.AssignmentService.Command
{
    public class UpdateAssignmentStateCommand : IRequest<Result<Task>>
    {
        public AssignmentStateUpdateModel StateUpdateModel { get; set; }
    }
    public class UpdateAssignmentStateCommandHandler : IRequestHandler<UpdateAssignmentStateCommand, Result<Task>>
    {
        private readonly ApplicationDbContext _context;

        public UpdateAssignmentStateCommandHandler (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Task>> Handle (UpdateAssignmentStateCommand command, CancellationToken cancellationToken)
        {
            var existAssignment = await _context.Assignments.Include(agn => agn.Staff).Include(agn => agn.Admin).Include(agn => agn.Asset).FirstOrDefaultAsync(agn => agn.AssignmentId == command.StateUpdateModel.AssignmentId);

            if (existAssignment == null)
            {
                return Result<Task>.Failure($"Assignment id : {command.StateUpdateModel.AssignmentId} not exist");
            }

            try
            {
                if (command.StateUpdateModel.AssignmentState == EnumsObject.AssignmentState.Accepted)
                {
                    existAssignment.State = EnumsObject.AssignmentState.Accepted.ToString();
                    existAssignment.Staff.IsAssigned = true;
                    existAssignment.Asset.State = EnumsObject.State.Assigned.ToString();
                    existAssignment.Asset.IsAssigned = true;
                }
                else if (command.StateUpdateModel.AssignmentState == EnumsObject.AssignmentState.Declined)
                {
                    existAssignment.State = EnumsObject.AssignmentState.Declined.ToString();
                    existAssignment.Asset.State = EnumsObject.State.Available.ToString();

                    if (!await _context.Assignments.AnyAsync(asg => asg.Staff.Id == existAssignment.Staff.Id))
                    {
                        existAssignment.Staff.IsAssigned = false;
                    }
                    existAssignment.Asset.IsAssigned = false;
                }
                else if (command.StateUpdateModel.AssignmentState == EnumsObject.AssignmentState.WaitingForAcceptance)
                {
                    existAssignment.State = EnumsObject.AssignmentState.WaitingForAcceptance.ToString();
                    existAssignment.Asset.State = EnumsObject.State.NotAvailable.ToString();
                }

                await _context.SaveChangesAsync(cancellationToken);
                return Result<Task>.Success(Task.CompletedTask);
            }
            catch (Exception ex)
            {
                return Result<Task>.Failure(ex.InnerException.Message);
            }
        }
    }
}
