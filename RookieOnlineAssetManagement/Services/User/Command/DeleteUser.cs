using MediatR;
using Microsoft.AspNetCore.Identity;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.UserService.Command
{
    public class DeleteUserCommand : IRequest<Result<Task>>
    {
        public string Id { get; set; }
        public string Location { get; set; }
    }
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<Task>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public DeleteUserCommandHandler (UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;

        }
        public async Task<Result<Task>> Handle (DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var existUser = await _userManager.FindByIdAsync(command.Id);

            if (existUser == null)
            {
                return Result<Task>.Failure($"User with id : {command.Id} does not found");
            }

            if (existUser.Location != command.Location)
            {
                return Result<Task>.Failure($"{command.Location} User can't delete {existUser.Location} User");
            }

            if (existUser.IsAssigned)
            {
                return Result<Task>.Failure($"Can't delete User that already assigned");
            }

            try
            {
                existUser.IsDisabled = true;
                await _context.SaveChangesAsync();
                return Result<Task>.Success(Task.CompletedTask);
            }
            catch (Exception ex)
            {
                return Result<Task>.Failure(ex.InnerException.Message);
            }

        }
    }
}
