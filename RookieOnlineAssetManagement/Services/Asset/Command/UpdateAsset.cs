using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.ViewModels.Asset;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.AssetService.Command
{
    public class UpdateAssetCommand : IRequest<Result<AssetReadModel>>
    {
        public AssetUpdateModel UpdateModel { get; set; }
        public string Location { get; set; }
        public class UpdateAssetCommandHandler : IRequestHandler<UpdateAssetCommand, Result<AssetReadModel>>
        {
            private readonly ApplicationDbContext _context;

            public UpdateAssetCommandHandler (ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result<AssetReadModel>> Handle (UpdateAssetCommand command, CancellationToken cancellationToken)
            {
                var existAsset = await _context.Assets.Include(ast => ast.Assignments).Include(ast => ast.Category).FirstOrDefaultAsync(ast => ast.AssestCode == command.UpdateModel.AssetCode);

                if (existAsset == null)
                {
                    return Result<AssetReadModel>.Failure($"Asset code : {command.UpdateModel.AssetCode} not exist");
                }

                if (existAsset.IsAssigned)
                {
                    return Result<AssetReadModel>.Failure($"You can't edit Asset that has already been assigned");
                }

                if (!existAsset.Assignments.Any(asgn => asgn.State == EnumsObject.AssignmentState.WaitingForAcceptance.ToString()))
                {
                    existAsset.State = command.UpdateModel.State.ToString();
                }

                try
                {
                    existAsset.AssestName = command.UpdateModel.AssestName;
                    existAsset.InstallDate = command.UpdateModel.InstallDate;
                    existAsset.Specification = command.UpdateModel.Specification;
                    existAsset.IsAssigned = command.UpdateModel.State == EnumsObject.State.Assigned ? true : false;

                    await _context.SaveChangesAsync();

                    return Result<AssetReadModel>.Success(new AssetReadModel
                    {
                        AssetCode = existAsset.AssestCode,
                        AssetName = existAsset.AssestName,
                        Location = existAsset.Location,
                        Specification = existAsset.Specification,
                        State = existAsset.State,
                        InstallDate = existAsset.InstallDate,
                        Category = existAsset.Category != null ? existAsset.Category.CategoryName : "",
                    });
                }
                catch (Exception ex)
                {
                    return Result<AssetReadModel>.Failure(ex.InnerException.Message);
                }

            }
        }
    }
}
