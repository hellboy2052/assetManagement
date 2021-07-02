using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.ViewModels.Asset;
using RookieOnlineAssetManagement.ViewModels.Assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.AssignmentService.Query
{
    public class GetFilteredAssignmentsQuery : IRequest<Result<AssignmentReadViewModel>>
    {
        public AssignmentFilterModel FilterModel { get; set; }
    }
    public class GetFilteredAssignmentsQueryHandler : IRequestHandler<GetFilteredAssignmentsQuery, Result<AssignmentReadViewModel>>
    {
        private readonly ApplicationDbContext _context;

        public GetFilteredAssignmentsQueryHandler (ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<AssignmentReadViewModel>> Handle (GetFilteredAssignmentsQuery query, CancellationToken cancellationToken)
        {
            var assignmentList = _context.Assignments.Include(ats => ats.Admin).Include(ats => ats.Staff).Include(ats => ats.Asset).AsEnumerable();

            GetMappedAssignmmentsWithAssignedDates(ref assignmentList, query.FilterModel.AssignedDate);

            if (query.FilterModel.States != null)
            {
                GetMappedAssignmmentsWithStates(ref assignmentList, query.FilterModel.States.Select(enu => enu.ToString()).ToArray());
            }

            var queryString = query.FilterModel.QueryString;
            var queryAssignedDate = query.FilterModel.AssignedDate;

            if (!string.IsNullOrWhiteSpace(queryString))
            {
                assignmentList = assignmentList.Where(
                asg => asg.Asset.AssestName.Contains(queryString) ||
                asg.Asset.AssestCode.StartsWith(queryString) ||
                asg.Staff.UserName.StartsWith(queryString));
            }

            if (queryAssignedDate is not null)
            {
                assignmentList = assignmentList.Where(asg => asg.AssignedDate == queryAssignedDate);
            }

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
                }).Skip((int)((query.FilterModel.PageIndex - 1) * query.FilterModel.PageSize))
                  .Take((int)query.FilterModel.PageSize);

            var totalItemsCount = assignmentList.Count();
            var totalPageCount = (int)Math.Ceiling((decimal)((double)totalItemsCount / query.FilterModel.PageSize));
            return Result<AssignmentReadViewModel>.Success(new AssignmentReadViewModel { TotalPages = totalPageCount, AssignmentReadModelList = resultMapped, TotalItems = totalItemsCount });

        }

        private void GetMappedAssignmmentsWithAssignedDates (ref IEnumerable<Assignment> assignmentList, DateTime? assignedDate)
        {
            if (assignedDate is not null)
            {
                assignmentList = assignmentList.Where(x => x.AssignedDate == (DateTime)assignedDate);
            }
        }

        private void GetMappedAssignmmentsWithStates (ref IEnumerable<Assignment> assignmentList, string[] stateNames)
        {
            assignmentList = assignmentList.Where(x => stateNames.Contains(x.State));
        }
    }
}

