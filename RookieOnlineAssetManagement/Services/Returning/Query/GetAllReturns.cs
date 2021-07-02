using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.ViewModels.Return;

namespace RookieOnlineAssetManagement.Services.ReturningService.Query
{
    public class GetAllReturnsQuery : IRequest<Result<IEnumerable<ReturnReadModel>>>
    {
        public class GetAllReturnsQueryHandler : IRequestHandler<GetAllReturnsQuery, Result<IEnumerable<ReturnReadModel>>>
        {

            private readonly ApplicationDbContext _context;
            public GetAllReturnsQueryHandler (ApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Result<IEnumerable<ReturnReadModel>>> Handle (GetAllReturnsQuery query, CancellationToken cancellationToken)
            {
                var returnList = _context.Returning.Select(rt => new ReturnReadModel
                {
                    ReturnId = rt.ReturnId,
                    AssetCode = rt.Assignment.AssetCode,
                    AssetName = rt.Assignment.Asset.AssestName,
                    AcceptedBy = rt.AssignedBy.UserName,
                    RequestedBy = rt.RequestBy.UserName,
                    AssignedDate = rt.Assignment.AssignedDate,
                    ReturnedDate = rt.ReturnedDate,
                    State = rt.State,
                });

                return Result<IEnumerable<ReturnReadModel>>.Success(returnList);

            }
        }
    }
}
