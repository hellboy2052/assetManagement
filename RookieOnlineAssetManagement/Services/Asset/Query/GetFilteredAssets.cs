using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.ViewModels.Asset;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.AssetService.Query
{
    public class GetFilteredAssetsQuery : IRequest<Result<AssetReadViewModel>>
    {
        public AssetFilterModel FilterModel { get; set; }
        public string Location { get; set; }


        public class GetFilteredAssetsQueryHandler : IRequestHandler<GetFilteredAssetsQuery, Result<AssetReadViewModel>>
        {
            private readonly ApplicationDbContext _context;

            public GetFilteredAssetsQueryHandler (ApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Result<AssetReadViewModel>> Handle (GetFilteredAssetsQuery query, CancellationToken cancellationToken)
            {
                var assetList = _context.Assets.Include(ats => ats.Category).Where(ats => ats.Location == query.Location).AsNoTracking();

                GetMappedAssetsWithCategories(ref assetList, query.FilterModel.Category, query.Location);

                if (query.FilterModel.States != null)
                {
                    GetMappedAssetsWithStates(ref assetList, query.FilterModel.States.Select(enu => enu.ToString()).ToArray(), query.Location);
                }

                var queryString = query.FilterModel.QueryString;

                if (!string.IsNullOrWhiteSpace(queryString))
                {
                    assetList = assetList.Where(
                    x => x.AssestCode.StartsWith(queryString) ||
                    x.AssestName.Contains(queryString));
                }

                var totalItemsCount = await assetList.CountAsync();
                var totalPageCount = (int)Math.Ceiling((decimal)((double)totalItemsCount / query.FilterModel.PageSize));

                var resultMapped = assetList.Select(x =>
                    new AssetReadModel
                    {
                        AssetCode = x.AssestCode,
                        AssetName = x.AssestName,
                        Location = query.Location,
                        Category = x.Category.CategoryName,
                        Specification = x.Specification,
                        State = x.State,
                        IsAssigned = x.IsAssigned,
                        InstallDate = x.InstallDate,
                    }).Skip((int)((query.FilterModel.PageIndex - 1) * query.FilterModel.PageSize))
                    .Take((int)query.FilterModel.PageSize);

                return Result<AssetReadViewModel>.Success(new AssetReadViewModel
                {
                    TotalPages = totalPageCount,
                    TotalItems = totalItemsCount,
                    AssetReadModelList = resultMapped,
                });
            }

            private void GetMappedAssetsWithCategories (ref IQueryable<Asset> assetList, string[] categoryNames, string adminLocation)
            {
                if (categoryNames != null)
                {
                    assetList = assetList.Where(x => x.Location == adminLocation && categoryNames.Contains(x.Category.CategoryName));
                }
            }

            private void GetMappedAssetsWithStates (ref IQueryable<Asset> assetList, string[] stateNames, string adminLocation)
            {
                if (stateNames != null)
                {
                    assetList = assetList.Where(x => x.Location == adminLocation && stateNames.Contains(x.State));
                }

            }
        }
    }
}

