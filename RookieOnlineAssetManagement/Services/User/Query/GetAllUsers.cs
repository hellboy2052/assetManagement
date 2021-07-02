using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.ViewModels.User;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.UserService.Query
{
    public class GetAllUsersQuery : IRequest<Result<UserReadViewModel>>
    {
        public PaginationModel PaginationModel { get; set; }
        public string Location { get; set; }
    }
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<UserReadViewModel>>
    {
        private readonly ApplicationDbContext _context;
        public GetAllUsersQueryHandler (ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<UserReadViewModel>> Handle (GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var userList = _context.Users.Where(x => !x.IsDisabled && x.Location == query.Location).AsNoTracking();

            if (query.PaginationModel.PageIndex is null)
            {
                query.PaginationModel.PageIndex = 1;
            }

            if (query.PaginationModel.PageSize is null)
            {
                query.PaginationModel.PageSize = 10;
            }

            var totalItemsCount = userList.Count();
            var totalPageCount = (int)Math.Ceiling((decimal)((double)totalItemsCount / query.PaginationModel.PageSize));

            var userMapped = userList
                .Skip((int)((query.PaginationModel.PageIndex - 1) * query.PaginationModel.PageSize))
                .Take((int)query.PaginationModel.PageSize)
                .Select(x => new UserReadModel
                {
                    Id = x.Id,
                    StaffCode = x.StaffCode,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    UserName = x.UserName,
                    JoinedDate = x.JoinedDate,
                    Location = x.Location,
                    DateOfBirth = x.DateOfBirth,
                    Gender = x.Gender,
                    Type = x.Type,
                    IsAssigned = x.IsAssigned,
                });

            return Result<UserReadViewModel>.Success(new UserReadViewModel { UserReadModelList = userMapped, TotalPages = totalPageCount, TotalItems = totalItemsCount });
        }
    }
}
