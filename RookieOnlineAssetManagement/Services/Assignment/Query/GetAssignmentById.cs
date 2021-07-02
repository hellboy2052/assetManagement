using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.ViewModels.Asset;
using RookieOnlineAssetManagement.ViewModels.Assignment;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.AssignmentService.Query
{
    public class GetAssignmentByIdQuery : IRequest<Result<AssignmentReadDetailModel>>
    {
        public string Id { get; set; }
    }
    public class GetAssignmentByIdQueryHandler : IRequestHandler<GetAssignmentByIdQuery, Result<AssignmentReadDetailModel>>
    {
        private readonly ApplicationDbContext _context;

        public GetAssignmentByIdQueryHandler (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<AssignmentReadDetailModel>> Handle (GetAssignmentByIdQuery query, CancellationToken cancellationToken)
        {
            var assignment = await _context.Assignments.Include(ats => ats.Admin).Include(ats => ats.Staff).Include(ats => ats.Asset)
                                           .AsNoTracking()
                                           .FirstOrDefaultAsync(x => x.AssignmentId == query.Id);
            if (assignment is null)
            {
                return Result<AssignmentReadDetailModel>.Failure($"Assignment {query.Id} not exist");
            }

            var assignmentMapped = new AssignmentReadDetailModel
            {
                AssetCode = assignment.Asset.AssestCode,
                AssetName = assignment.Asset.AssestName,
                Specification = assignment.Asset.Specification,
                Note = assignment.Note,
                State = assignment.State,
                AssignedBy = assignment.Admin.UserName,
                AssignedTo = assignment.Staff.UserName,
                AssignedDate = assignment.AssignedDate,
            };

            return Result<AssignmentReadDetailModel>.Success(assignmentMapped);
        }
    }
}
