using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;

namespace RookieOnlineAssetManagement.Services.Security
{
    public class IsDisableRequirement : IAuthorizationRequirement
    {

    }

    public class IsDisableRequirementHandler : AuthorizationHandler<IsDisableRequirement>
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IsDisableRequirementHandler(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsDisableRequirement requirement)
        {
            var userid = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userid == null)
            {
                return Task.CompletedTask;
            }

            var user = _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == userid)
                .Result;
            if (user == null)
            {
                return Task.CompletedTask;
            }

            if (!user.IsDisabled)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}