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
    public class GetAssignmentEditDetailByIdQuery : IRequest<Result<AssignmentReadEditDetailModel>>
    {
        public string Id { get; set; }
    }
    public class GetAssignmentEditDetailByIdHandler : IRequestHandler<GetAssignmentEditDetailByIdQuery, Result<AssignmentReadEditDetailModel>>
    {
        private readonly ApplicationDbContext _context;

        public GetAssignmentEditDetailByIdHandler (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<AssignmentReadEditDetailModel>> Handle (GetAssignmentEditDetailByIdQuery query, CancellationToken cancellationToken)
        {
            var assignment = await _context.Assignments.Include(ats => ats.Staff).Include(ats => ats.Asset)
                                           .AsNoTracking()
                                           .FirstOrDefaultAsync(x => x.AssignmentId == query.Id);
            if (assignment is null)
            {
                return Result<AssignmentReadEditDetailModel>.Failure($"Assignment {query.Id} not exist");
            }

            var assignmentMapped = new AssignmentReadEditDetailModel
            {
                AssignmentId = assignment.AssignmentId,
                UserId = assignment.StaffId,
                UserName = assignment.Staff.FullName,
                AssetCode = assignment.AssetCode,
                AssetName = assignment.Asset.AssestName,
                Note = assignment.Note,

                AssignedDate = assignment.AssignedDate,
            };

            return Result<AssignmentReadEditDetailModel>.Success(assignmentMapped);
        }
    }
}
