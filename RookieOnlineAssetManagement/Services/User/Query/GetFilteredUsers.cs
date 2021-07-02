using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Constants;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.ViewModels.User;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.UserService.Query
{
    public class GetFilteredUsersQuery : IRequest<Result<UserReadViewModel>>
    {
        public UserFilterModel FilterModel { get; set; }
        public string UserId { get; set; }


    }
    public class GetFilteredUsersQueryHandler : IRequestHandler<GetFilteredUsersQuery, Result<UserReadViewModel>>
    {
        private readonly ApplicationDbContext _context;
        public GetFilteredUsersQueryHandler (ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<UserReadViewModel>> Handle (GetFilteredUsersQuery query, CancellationToken cancellationToken)
        {

            var userList = GetMappedUsersWithRoles(query.FilterModel.Type, query.UserId);

            var queryString = query.FilterModel.QueryString;

            if (!string.IsNullOrWhiteSpace(queryString))
            {
                if (queryString.StartsWith(ConstantsObject.StaffCodeCharacter))
                {
                    userList = userList.Where(x => x.StaffCode.StartsWith(queryString));
                }
                else
                {
                    userList = userList.Where(x => x.FullName.Contains(queryString));
                }
            }

            var totalItemsCount = userList.Count();
            var totalPageCount = (int)Math.Ceiling((decimal)((double)totalItemsCount / query.FilterModel.PageSize));
            var userMapped = userList
                .Skip((int)((query.FilterModel.PageIndex - 1) * query.FilterModel.PageSize))
                .Take((int)query.FilterModel.PageSize).Select(x => new UserReadModel
                {
                    Id = x.Id,
                    StaffCode = x.StaffCode,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    UserName = x.UserName,
                    JoinedDate = x.JoinedDate,
                    Location = x.Location,
                    IsAssigned = x.IsAssigned,
                    DateOfBirth = x.DateOfBirth,
                    Gender = x.Gender,
                    Type = x.Type,
                });
            return Result<UserReadViewModel>.Success(new UserReadViewModel
            {
                TotalPages = totalPageCount,
                UserReadModelList = userMapped,
                TotalItems = totalItemsCount,
            });
        }

        private IQueryable<ApplicationUser> GetMappedUsersWithRoles (string[] roleNames, string userId)
        {
            var currentLoggedInUser = _context.Users.FirstOrDefault(usr => usr.Id == userId);
            //nếu roleId = null thì sẽ load tất cả user của tất cả các role
            IQueryable<ApplicationUser> userList = _context.Users.Where(x => !x.IsDisabled && x.Id != userId && x.Location == currentLoggedInUser.Location).AsNoTracking();

            if (roleNames == null || roleNames.Length == 0)
            {
                return userList;
            }

            return userList.Where(x => roleNames.Contains(x.Type));
        }
    }
}

