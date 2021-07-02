using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Utils;
using RookieOnlineAssetManagement.ViewModels.Asset;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.AssetService.Command
{
    public class CreateAssetCommand : IRequest<Result<AssetReadModel>>
    {
        public AssetCreateModel CreateModel { get; set; }
        public string Location { get; set; }
        public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, Result<AssetReadModel>>
        {
            private readonly ApplicationDbContext _context;

            public CreateAssetCommandHandler (ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result<AssetReadModel>> Handle (CreateAssetCommand command, CancellationToken cancellationToken)
            {
                var existingCate = await _context.Categories.FirstOrDefaultAsync(cate => cate.CategoryId == command.CreateModel.CategoryId);

                if (existingCate == null)
                {
                    return Result<AssetReadModel>.Failure($"Category Id : {command.CreateModel.CategoryId} not exist");
                }

                var existingAssetCode = _context.Assets
                    .Where(ast => ast.AssestCode
                    .StartsWith(command.CreateModel.CategoryId));

                int incrementId = 1;

                if (existingAssetCode.Any())
                {
                    incrementId = await existingAssetCode.MaxAsync(ast => Convert.ToInt32(ast.AssestCode.Substring(ast.AssestCode.Length - 6, 6))) + 1;

                }
                var generatedAssetCode = Generator.GenerateAssetCode(command.CreateModel.CategoryId, incrementId);

                var assetToBeAdded = new Asset
                {
                    AssestCode = generatedAssetCode,
                    AssestName = command.CreateModel.AssestName,
                    InstallDate = command.CreateModel.InstallDate,
                    Location = command.Location,
                    CategoryId = command.CreateModel.CategoryId,
                    Specification = command.CreateModel.Specification,
                    State = command.CreateModel.State.ToString(),
                };

                try
                {
                    await _context.Assets.AddAsync(assetToBeAdded);
                    existingCate.Assets.Add(assetToBeAdded);
                    await _context.SaveChangesAsync();

                    assetToBeAdded = await _context.Assets.FirstOrDefaultAsync(ast => ast.AssestCode == generatedAssetCode);

                    return Result<AssetReadModel>.Success(new AssetReadModel
                    {
                        AssetCode = assetToBeAdded.AssestCode,
                        AssetName = assetToBeAdded.AssestName,
                        Location = assetToBeAdded.Location,
                        Specification = assetToBeAdded.Specification,
                        State = assetToBeAdded.State,
                        InstallDate = assetToBeAdded.InstallDate,
                        Category = assetToBeAdded.Category.CategoryName,
                        IsAssigned = assetToBeAdded.IsAssigned,
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
