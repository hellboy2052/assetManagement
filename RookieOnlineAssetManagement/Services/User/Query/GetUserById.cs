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
    public class GetUserByIdQuery : IRequest<Result<UserReadModel>>
    {
        public string Id { get; set; }
        public string Location { get; set; }
    }
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserReadModel>>
    {
        private readonly ApplicationDbContext _context;
        public GetUserByIdQueryHandler (ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<UserReadModel>> Handle (GetUserByIdQuery query, CancellationToken cancellationToken)
        {

            var user = await _context.Users
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => !x.IsDisabled && x.Id == query.Id && x.Location == query.Location);

            if (user is null)
            {
                return Result<UserReadModel>.Failure($"Users {query.Id} not exist");
            }
            var userMapped = new UserReadModel
            {
                Id = user.Id,
                StaffCode = user.StaffCode,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                JoinedDate = user.JoinedDate,
                Location = user.Location,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                Type = user.Type,
                IsAssigned = user.IsAssigned,
            };

            return Result<UserReadModel>.Success(userMapped);
        }
    }
}
