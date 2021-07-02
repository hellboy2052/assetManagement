using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.ViewModels.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.ReturningService.Query
{
    public class GetFilteredReturningQuery : IRequest<Result<IEnumerable<ReturnReadModel>>>
    {
        public ReturnFilteredModel FilterModel { get; set; }
    }
    public class GetFilteredReturningQueryHandler : IRequestHandler<GetFilteredReturningQuery, Result<IEnumerable<ReturnReadModel>>>
    {
        private readonly ApplicationDbContext _context;

        public GetFilteredReturningQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<IEnumerable<ReturnReadModel>>> Handle(GetFilteredReturningQuery query, CancellationToken cancellationToken)
        {
            var returningList = _context.Returning.Include(rtn => rtn.Assignment).ThenInclude(asgn => asgn.Asset).Include(rtn => rtn.RequestBy).Include(rtn => rtn.AssignedBy).AsEnumerable();

            GetMappedReturningWithReturnedDates(ref returningList, query.FilterModel.ReturnedDate);

            if (query.FilterModel.States != null)
            {
                GetMappedReturningWithStates(ref returningList, query.FilterModel.States.Select(enu => enu.ToString()).ToArray());
            }


            var queryString = query.FilterModel.QueryString;
            var queryReturnedDate = query.FilterModel.ReturnedDate;

            if (!string.IsNullOrWhiteSpace(queryString))
            {
                returningList = returningList.Where(
                rtn => rtn.Assignment.Asset.AssestName.Contains(queryString) ||
                rtn.Assignment.Asset.AssestCode.StartsWith(queryString) ||
                rtn.RequestBy.UserName.StartsWith(queryString));
            }

            if (queryReturnedDate is not null)
            {
                returningList = returningList.Where(rtn => rtn.ReturnedDate == queryReturnedDate);
            }

            var resultMapped = returningList.Select(x =>
                new ReturnReadModel
                {
                    ReturnId = x.ReturnId,
                    AssetCode = x.Assignment.Asset.AssestCode,
                    AssetName = x.Assignment.Asset.AssestName,
                    RequestedBy = x.RequestBy.UserName,
                    AcceptedBy = x.AssignedBy != null ? x.AssignedBy.UserName : null,
                    AssignedDate = x.Assignment.AssignedDate,
                    ReturnedDate = x.ReturnedDate,
                    State = x.State,

                });


            return Result<IEnumerable<ReturnReadModel>>.Success(resultMapped);

        }

        private void GetMappedReturningWithReturnedDates(ref IEnumerable<Returning> returningList, DateTime? returnedDate)
        {
            if (returnedDate is not null)
            {
                returningList = returningList.Where(x => x.ReturnedDate == (DateTime)returnedDate);
            }
        }

        private void GetMappedReturningWithStates(ref IEnumerable<Returning> returningList, string[] stateNames)
        {
            returningList = returningList.Where(x => stateNames.Contains(x.State));
        }
    }
}

