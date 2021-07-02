using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.ViewModels.Assignment;
using RookieOnlineAssetManagement.ViewModels.User;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.AssignmentService.Query
{
    public class GetAllAssignmentQuery : IRequest<Result<AssignmentReadViewModel>>
    {
        public PaginationModel PaginationModel { get; set; }
        public string Location { get; set; }
    }
    public class GetAllAssignmentQueryHandler : IRequestHandler<GetAllAssignmentQuery, Result<AssignmentReadViewModel>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllAssignmentQueryHandler (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<AssignmentReadViewModel>> Handle (GetAllAssignmentQuery query, CancellationToken cancellationToken)
        {
            var assignmentList = _context.Assignments.Include(ats => ats.Admin).Include(ats => ats.Staff).Include(ats => ats.Asset);
            var resultMapped = assignmentList.Select(x =>
                new AssignmentReadModel
                {
                    AssetCode = x.Asset.AssestCode,
                    AssetName = x.Asset.AssestName,
                    AssignmentId = x.AssignmentId,
                    State = x.State,
                    AssignedBy = x.Admin.UserName,
                    AssignedTo = x.Staff.UserName,
                    AssignedDate = x.AssignedDate,
                });

            var totalItemsCount = assignmentList.Count();
            var totalPage = (int)Math.Ceiling((decimal)((double)totalItemsCount / query.PaginationModel.PageSize));
            return Result<AssignmentReadViewModel>.Success(new AssignmentReadViewModel { TotalPages = totalPage, AssignmentReadModelList = resultMapped });

        }
    }
}
