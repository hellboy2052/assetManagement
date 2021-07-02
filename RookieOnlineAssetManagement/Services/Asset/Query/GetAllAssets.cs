using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.ViewModels.Asset;
using RookieOnlineAssetManagement.ViewModels.User;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.AssetService.Query
{
    public class GetAllAssetsQuery : IRequest<Result<AssetReadViewModel>>
    {
        public PaginationModel PaginationModel { get; set; }
        public string Location { get; set; }
        public class GetAllAssetsQueryHandler : IRequestHandler<GetAllAssetsQuery, Result<AssetReadViewModel>>
        {
            private readonly ApplicationDbContext _context;

            public GetAllAssetsQueryHandler (ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result<AssetReadViewModel>> Handle (GetAllAssetsQuery query, CancellationToken cancellationToken)
            {
                var assetList = _context.Assets.Include(ats => ats.Category).Where(x => x.Location == query.Location).AsNoTracking();

                var totalItemsCount = await assetList.CountAsync();
                var totalPageCount = (int)Math.Ceiling((decimal)((double)totalItemsCount / query.PaginationModel.PageSize));

                var assetMapped = assetList
                    .Skip((int)((query.PaginationModel.PageIndex - 1) * query.PaginationModel.PageSize))
                    .Take((int)query.PaginationModel.PageSize)
                    .Select(x => new AssetReadModel
                    {
                        AssetCode = x.AssestCode,
                        AssetName = x.AssestName,
                        Location = query.Location,
                        Category = x.Category.CategoryName,
                        Specification = x.Specification,
                        State = x.State,
                        IsAssigned = x.IsAssigned,
                        InstallDate = x.InstallDate
                    });

                return Result<AssetReadViewModel>.Success(new AssetReadViewModel { AssetReadModelList = assetMapped, TotalPages = totalPageCount, TotalItems = totalItemsCount });
            }
        }
    }
}
