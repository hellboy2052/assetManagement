using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.ViewModels.Asset;
using RookieOnlineAssetManagement.ViewModels.History;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.AssetService.Query
{
    public class GetAssetDetailQuery : IRequest<Result<AssetDetailReadModel>>
    {
        public string AssetCode { get; set; }
        public string Location { get; set; }
        public class GetAssetDetailQueryHandler : IRequestHandler<GetAssetDetailQuery, Result<AssetDetailReadModel>>
        {
            private readonly ApplicationDbContext _context;

            public GetAssetDetailQueryHandler (ApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Result<AssetDetailReadModel>> Handle (GetAssetDetailQuery query, CancellationToken cancellationToken)
            {
                var existingAsset = await _context.Assets
                    .Include(ats => ats.Category)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(ast => ast.AssestCode == query.AssetCode &&
                    ast.Location == query.Location);

                if (existingAsset == null)
                {
                    return Result<AssetDetailReadModel>.Failure($"Asset Code : {query.AssetCode} not exist");
                }

                var histories = _context.Returning.Where(rtn => rtn.Assignment.AssetCode == existingAsset.AssestCode);

                var resultMapped =
                    new AssetDetailReadModel
                    {
                        AssetCode = existingAsset.AssestCode,
                        AssetName = existingAsset.AssestName,
                        Location = query.Location,
                        Category = existingAsset.Category.CategoryName,
                        Specification = existingAsset.Specification,
                        State = existingAsset.State,
                        InstallDate = existingAsset.InstallDate,
                        IsAssigned = existingAsset.IsAssigned,
                    };

                if (histories != null)
                {
                    resultMapped.Histories = histories.Select(hst =>
                    new HistoryReadModel
                    {
                        AssignedBy = hst.AssignedBy.UserName,
                        AssignedTo = hst.RequestBy.UserName,
                        AssignedDate = hst.Assignment.AssignedDate,
                        ReturnedDate = hst.ReturnedDate,

                    });
                }
                return Result<AssetDetailReadModel>.Success(resultMapped);
            }
        }
    }
}

