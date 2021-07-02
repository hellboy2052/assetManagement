using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.ViewModels.Assignment;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.AssignmentService.Command
{
    public class CreateAssignmentCommand : IRequest<Result<AssignmentReadModel>>
    {
        public AssignmentCreateModel CreateModel { get; set; }
        public string AdminId { get; set; }
    }
    public class CreateAssignmentCommandHandler : IRequestHandler<CreateAssignmentCommand, Result<AssignmentReadModel>>
    {
        private readonly ApplicationDbContext _context;

        public CreateAssignmentCommandHandler (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<AssignmentReadModel>> Handle (CreateAssignmentCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(usr => usr.Id == command.CreateModel.UserId);
            var existingAdmin = await _context.Users.FirstOrDefaultAsync(usr => usr.Id == command.AdminId);
            var existingAsset = await _context.Assets.Include(ast => ast.Assignments).FirstOrDefaultAsync(ast => ast.AssestCode == command.CreateModel.AssestCode);

            if (existingAdmin == null)
            {
                return Result<AssignmentReadModel>.Failure($"Admin id : {command.AdminId} not exist");
            }

            if (existingUser == null)
            {
                return Result<AssignmentReadModel>.Failure($"User id : {command.CreateModel.UserId} not exist");
            }

            if (existingAsset == null)
            {
                return Result<AssignmentReadModel>.Failure($"Asset code : {command.CreateModel.AssestCode} not exist");
            }

            if (existingAsset.Assignments.Any(asgn => asgn.StaffId == existingUser.Id && existingAsset.State != EnumsObject.State.Available.ToString()))
            {
                return Result<AssignmentReadModel>.Failure($"Assignment with this Asset code is already exist");
            }

            var assignmentToBeAdded = new Assignment
            {
                AssetCode = command.CreateModel.AssestCode,
                StaffId = command.CreateModel.UserId,
                AdminId = command.AdminId,
                AssignedDate = command.CreateModel.AssignedDate,
                Note = command.CreateModel.Note,
                State = EnumsObject.AssignmentState.WaitingForAcceptance.ToString(),
            };

            try
            {
                existingAsset.State = EnumsObject.State.NotAvailable.ToString();
                await _context.Assignments.AddAsync(assignmentToBeAdded, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Result<AssignmentReadModel>.Success(new AssignmentReadModel
                {
                    AssignmentId = assignmentToBeAdded.AssignmentId,
                    AssetCode = assignmentToBeAdded.AssetCode,
                    AssetName = assignmentToBeAdded.Asset.AssestName,
                    State = assignmentToBeAdded.State.ToString(),
                    AssignedBy = assignmentToBeAdded.Admin.UserName,
                    AssignedTo = assignmentToBeAdded.Staff.UserName,
                    AssignedDate = assignmentToBeAdded.AssignedDate,
                });
            }
            catch (Exception ex)
            {
                return Result<AssignmentReadModel>.Failure(ex.InnerException.Message);
            }

        }
    }
}
