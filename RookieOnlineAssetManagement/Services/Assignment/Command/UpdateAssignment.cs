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
    public class UpdateAssignmentCommand : IRequest<Result<AssignmentReadDetailModel>>
    {
        public AssignmentUpdateModel UpdateModel { get; set; }
        public string AdminId { get; set; }
    }
    public class UpdateAssignmentCommandHandler : IRequestHandler<UpdateAssignmentCommand, Result<AssignmentReadDetailModel>>
    {
        private readonly ApplicationDbContext _context;

        public UpdateAssignmentCommandHandler (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<AssignmentReadDetailModel>> Handle (UpdateAssignmentCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(usr => usr.Id == command.UpdateModel.UserId);
            var existingAsset = await _context.Assets.FirstOrDefaultAsync(ast => ast.AssestCode == command.UpdateModel.AssestCode);
            var existingAssignment = await _context.Assignments.Include(asg => asg.Admin).Include(asg => asg.Asset).FirstOrDefaultAsync(asg => asg.AssignmentId == command.UpdateModel.AssignmentId);

            if (existingAssignment == null)
            {
                return Result<AssignmentReadDetailModel>.Failure($"Assignment id : {command.UpdateModel.AssignmentId} not exist");
            }

            if (existingAssignment.State != EnumsObject.AssignmentState.WaitingForAcceptance.ToString())
            {
                return Result<AssignmentReadDetailModel>.Failure($"Only assignments have state waiting for acceptance is able to update");
            }

            if (existingUser == null)
            {
                return Result<AssignmentReadDetailModel>.Failure($"User id : {command.UpdateModel.UserId} not exist");
            }

            if (existingAsset == null)
            {
                return Result<AssignmentReadDetailModel>.Failure($"Asset code : {command.UpdateModel.AssestCode} not exist");
            }

            if (existingAsset.AssestCode != existingAssignment.AssetCode && existingAsset.IsAssigned)
            {
                return Result<AssignmentReadDetailModel>.Failure($"You can't select Asset that has already been assigned");
            }


            try
            {
                existingAssignment.StaffId = command.UpdateModel.UserId;
                existingAssignment.AdminId = command.AdminId;
                if (command.UpdateModel.AssignedDate != existingAssignment.AssignedDate.Date && command.UpdateModel.AssignedDate >= DateTime.Now.Date)
                {
                    existingAssignment.AssignedDate = command.UpdateModel.AssignedDate;
                }
                existingAssignment.Note = command.UpdateModel.Note;
                existingAssignment.State = EnumsObject.AssignmentState.WaitingForAcceptance.ToString();
                existingAssignment.Asset.IsAssigned = false;
                existingAssignment.Asset.State = EnumsObject.State.Available.ToString();
                existingAssignment.AssetCode = command.UpdateModel.AssestCode;

                existingAsset.IsAssigned = true;
                existingAsset.State = EnumsObject.State.NotAvailable.ToString();

                await _context.SaveChangesAsync(cancellationToken);

                return Result<AssignmentReadDetailModel>.Success(new AssignmentReadDetailModel
                {
                    AssetCode = existingAssignment.AssetCode,
                    AssetName = existingAssignment.Asset.AssestName,
                    Specification = existingAssignment.Asset.Specification,
                    State = existingAssignment.State,
                    AssignedBy = existingAssignment.Admin.UserName,
                    AssignedTo = existingAssignment.Staff.UserName,
                    AssignedDate = existingAssignment.AssignedDate,
                    Note = existingAssignment.Note,
                });
            }
            catch (Exception ex)
            {
                return Result<AssignmentReadDetailModel>.Failure(ex.InnerException.Message);
            }
        }
    }
}
