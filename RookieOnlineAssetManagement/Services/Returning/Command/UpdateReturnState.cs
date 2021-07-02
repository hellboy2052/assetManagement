using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.ViewModels.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.ReturningService.Command
{
    public class UpdateReturnStateCommand : IRequest<Result<Task>>
    {
        public ReturnStateUpdateModel returnStateUpdateModel { get; set; }
        public string UserId { get; set; }
        public class UpdateReturnStateCommandHandler : IRequestHandler<UpdateReturnStateCommand, Result<Task>>
        {
            private readonly ApplicationDbContext _context;
            public UpdateReturnStateCommandHandler (ApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Result<Task>> Handle (UpdateReturnStateCommand command, CancellationToken cancellationToken)
            {
                var existingReturning = await _context.Returning
                    .Include(rt => rt.Assignment).ThenInclude(asg => asg.Asset)
                    .Include(rt => rt.Assignment).ThenInclude(asg => asg.Staff)
                    .Include(rt => rt.RequestBy)
                    .FirstOrDefaultAsync(rt => rt.ReturnId == command.returnStateUpdateModel.ReturnId);

                if (existingReturning == null)
                {
                    return Result<Task>.Failure($"ReturningId: {command.returnStateUpdateModel.ReturnId} is not exist");
                }
                if (existingReturning.State != EnumsObject.Returning.WaitingForReturning.ToString())
                {
                    return Result<Task>.Failure($"Only Waiting for Returning is able to Completed");
                }

                try
                {
                    //Kiểm tra các Assignment state = accepted hoặc waiting và có userId = userId returning đang complete
                    var isExisting = await _context.Assignments.CountAsync(
                        asg => (asg.State == EnumsObject.AssignmentState.Accepted.ToString()
                        || asg.State == EnumsObject.AssignmentState.WaitingForReturning.ToString())
                        && asg.StaffId == existingReturning.Assignment.StaffId);

                    if (isExisting == 1)
                    {
                        //Nếu không có thì disable user được
                        existingReturning.Assignment.Staff.IsAssigned = false;
                    }
                    else
                    {
                        existingReturning.Assignment.Staff.IsAssigned = true;
                    }

                    existingReturning.Assignment.State = EnumsObject.AssignmentState.Completed.ToString();
                    existingReturning.State = EnumsObject.Returning.Completed.ToString();
                    existingReturning.Assignment.Asset.State = EnumsObject.State.Available.ToString();
                    existingReturning.AssignedById = command.UserId;
                    existingReturning.ReturnedDate = DateTime.Now;

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
}