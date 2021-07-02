using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Entities;
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
    public class CreateReturnCommand : IRequest<Result<ReturnReadModel>>
    {
        public ReturnCreateModel CreateModel { get; set; }
        public string UserId { get; set; }
        public class CreateReturnCommandHandler : IRequestHandler<CreateReturnCommand, Result<ReturnReadModel>>
        {
            private readonly ApplicationDbContext _context;
            public CreateReturnCommandHandler (ApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Result<ReturnReadModel>> Handle (CreateReturnCommand command, CancellationToken cancellationToken)
            {
                var existingAssignment = await _context.Assignments.Include(asg => asg.Asset).FirstOrDefaultAsync(asg => asg.AssignmentId == command.CreateModel.AssignmentId);

                if (existingAssignment == null)
                {
                    return Result<ReturnReadModel>.Failure($"Assignment id : {command.CreateModel.AssignmentId} is not exist");
                }
                if (existingAssignment.State != EnumsObject.AssignmentState.Accepted.ToString())
                {
                    return Result<ReturnReadModel>.Failure("Only Assignment has Accepted state is allow to Create Request for Returning");
                }

                existingAssignment.State = EnumsObject.AssignmentState.WaitingForReturning.ToString();

                var returningToBeAdded = new Returning
                {
                    RequestById = command.UserId,
                    State = EnumsObject.Returning.WaitingForReturning.ToString(),
                    Assignment = existingAssignment,
                };


                try
                {
                    await _context.Returning.AddAsync(returningToBeAdded, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    await _context.Entry(returningToBeAdded).Reference(rtn => rtn.RequestBy).LoadAsync();

                    return Result<ReturnReadModel>.Success(new ReturnReadModel
                    {
                        ReturnId = returningToBeAdded.ReturnId,
                        AssetCode = returningToBeAdded.Assignment.AssetCode,
                        AssetName = returningToBeAdded.Assignment.Asset.AssestName,
                        RequestedBy = returningToBeAdded.RequestBy.UserName,
                        State = returningToBeAdded.State,
                        AssignedDate = returningToBeAdded.Assignment.AssignedDate,
                    });
                }

                catch (Exception e)
                {
                    return Result<ReturnReadModel>.Failure(e.InnerException.Message);
                }
            }
        }
    }
}
