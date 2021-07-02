using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.ViewModels.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.Report.Query
{
    public class GetReportQuery : IRequest<Result<IEnumerable<ReportReadModel>>>
    {
        public string Location { get; set; }
    }
    public class GetReportQueryHandler : IRequestHandler<GetReportQuery, Result<IEnumerable<ReportReadModel>>>
    {
        private readonly ApplicationDbContext _context;
        public GetReportQueryHandler (ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<IEnumerable<ReportReadModel>>> Handle (GetReportQuery query, CancellationToken cancellationToken)
        {
            var groupedAsset = _context.Assets
                .Where(ast => ast.Location == query.Location)
                .GroupBy(ast => ast.Category.CategoryName)
                .Select(g => new ReportReadModel
                {
                    CategoryName = g.Key,
                    TotalAssets = g.Count(),
                    AssignedAssets = g.Count(ast => ast.State == EnumsObject.State.Assigned.ToString()),
                    AvailableAssets = g.Count(ast => ast.State == EnumsObject.State.Available.ToString()),
                    NotAvailableAssets = g.Count(ast => ast.State == EnumsObject.State.NotAvailable.ToString()),
                    WaitingForRecyclingAssets = g.Count(ast => ast.State == EnumsObject.State.WaitingForRecycling.ToString()),
                    RecycledAssets = g.Count(ast => ast.State == EnumsObject.State.Recycled.ToString()),
                });


            return Result<IEnumerable<ReportReadModel>>.Success(groupedAsset);
        }
    }
}
