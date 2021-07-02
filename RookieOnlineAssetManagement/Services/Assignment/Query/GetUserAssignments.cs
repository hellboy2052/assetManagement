using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.ViewModels.Asset;
using RookieOnlineAssetManagement.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.AssignmentService.Query
{
    public class GetUserAssignmentsQuery : IRequest<Result<IEnumerable<UserAssignmentReadModel>>>
    {
        public string UserId { get; set; }
    }
    public class GetUserAssignmentsQueryHandler : IRequestHandler<GetUserAssignmentsQuery, Result<IEnumerable<UserAssignmentReadModel>>>
    {
        private readonly ApplicationDbContext _context;

        public GetUserAssignmentsQueryHandler (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<UserAssignmentReadModel>>> Handle (GetUserAssignmentsQuery query, CancellationToken cancellationToken)
        {
            var currentUserAssignments = _context.Assignments
                .Include(asg => asg.Asset)
                .Where(asg => asg.StaffId == query.UserId &&
                asg.AssignedDate <= DateTime.Now && (
                asg.State == EnumsObject.AssignmentState.Accepted.ToString() ||
                asg.State == EnumsObject.AssignmentState.WaitingForAcceptance.ToString())).AsNoTracking();

            var assignmentsMapped = currentUserAssignments
                .Select(asg => new
                UserAssignmentReadModel
                {
                    AssignmentId = asg.AssignmentId,
                    AssetName = asg.Asset.AssestName,
                    AssetCode = asg.Asset.AssestCode,
                    Category = asg.Asset.Category.CategoryName,
                    AssignedDate = asg.AssignedDate,
                    State = asg.State
                });

            return Result<IEnumerable<UserAssignmentReadModel>>.Success(assignmentsMapped);
        }
    }
}
