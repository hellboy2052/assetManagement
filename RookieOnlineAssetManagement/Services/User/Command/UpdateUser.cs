using MediatR;
using Microsoft.AspNetCore.Identity;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.ViewModels.User;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.UserService.Command
{
    public class UpdateUserCommand : IRequest<Result<UserReadModel>>
    {
        public UserUpdateModel UpdateModel { get; set; }
        public string Location { get; set; }
    }
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<UserReadModel>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UpdateUserCommandHandler (UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }

        public async Task<Result<UserReadModel>> Handle (UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var existUser = await _userManager.FindByIdAsync(command.UpdateModel.Id);

            if (existUser == null)
            {
                return Result<UserReadModel>.Failure($"User with id : {command.UpdateModel.Id} does not found");
            }

            if (existUser.Location != command.Location)
            {
                return Result<UserReadModel>.Failure($"This User location is not the same with Admin");
            }

            var updateRole = await _roleManager.FindByNameAsync(command.UpdateModel.Type.ToString());

            if (updateRole == null)
            {
                return Result<UserReadModel>.Failure($"Role : {command.UpdateModel.Type} not found");
            }

            var existUserRoles = await _userManager.GetRolesAsync(existUser);

            if (!existUserRoles.Contains(updateRole.Name))
            {
                await _userManager.RemoveFromRolesAsync(existUser, existUserRoles);
                await _userManager.AddToRoleAsync(existUser, updateRole.Name);
            }

            try
            {
                existUser.DateOfBirth = command.UpdateModel.DateOfBirth;
                existUser.Gender = command.UpdateModel.Gender;
                existUser.Type = command.UpdateModel.Type.ToString();
                existUser.JoinedDate = command.UpdateModel.JoinedDate;
                await _userManager.UpdateAsync(existUser);

                return Result<UserReadModel>.Success(new UserReadModel
                {
                    Id = existUser.Id,
                    FirstName = existUser.FirstName,
                    LastName = existUser.LastName,
                    UserName = existUser.UserName,
                    JoinedDate = existUser.JoinedDate,
                    DateOfBirth = existUser.DateOfBirth,
                    Gender = existUser.Gender,
                    Location = existUser.Location,
                    StaffCode = existUser.StaffCode,
                    Type = updateRole.Name,
                });
            }
            catch (Exception ex)
            {
                return Result<UserReadModel>.Failure(ex.InnerException.Message);
            }

        }
    }
}
