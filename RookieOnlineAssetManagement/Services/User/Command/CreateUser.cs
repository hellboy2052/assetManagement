using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Utils;
using RookieOnlineAssetManagement.ViewModels.User;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.UserService.Command
{
    public class CreateUserCommand : IRequest<Result<UserReadModel>>
    {
        public UserCreateModel CreateModel { get; set; }

        public string Location { get; set; }
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserReadModel>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public CreateUserCommandHandler (UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<Result<UserReadModel>> Handle (CreateUserCommand command, CancellationToken cancellationToken)
        {
            var existRole = await _roleManager.FindByNameAsync(command.CreateModel.Type.ToString());

            if (existRole == null)
            {
                return Result<UserReadModel>.Failure($"Role : {command.CreateModel.Type} not found");
            }

            var generatedUserName = Generator.GenerateUserName(command.CreateModel.FirstName, command.CreateModel.LastName);
            var isUserNameExist = await _userManager.Users.AnyAsync(usr => usr.UserName == generatedUserName);

            if (isUserNameExist)
            {
                int incrementId = await _userManager.Users.CountAsync(usr => usr.UserName.Contains(generatedUserName));
                generatedUserName = Generator.AppendIdToUserName(generatedUserName, incrementId);
            }


            var userToBeAddedLocation = command.CreateModel.Location == null ? command.Location : command.CreateModel.Location.ToString();
            var userToBeAdded = new ApplicationUser
            {
                FirstName = command.CreateModel.FirstName,
                LastName = command.CreateModel.LastName,
                Location = userToBeAddedLocation,
                DateOfBirth = command.CreateModel.DoB,
                Gender = command.CreateModel.Gender,
                JoinedDate = command.CreateModel.JoinedDate,
                UserName = generatedUserName,
                Type = existRole.Name,
            };

            try
            {
                var generatedPassword = $"{generatedUserName}@{command.CreateModel.DoB:ddMMyyyy}";
                var createResult = await _userManager.CreateAsync(userToBeAdded, generatedPassword);

                if (!createResult.Succeeded)
                {
                    return Result<UserReadModel>.Failure(createResult.Errors.ToString());
                }

                userToBeAdded = await _userManager.FindByNameAsync(generatedUserName);
                await _userManager.AddToRoleAsync(userToBeAdded, existRole.Name);
                await _userManager.AddClaimAsync(userToBeAdded, new System.Security.Claims.Claim("Location", userToBeAdded.Location));

                return Result<UserReadModel>.Success(new UserReadModel
                {
                    Id = userToBeAdded.Id,
                    FirstName = userToBeAdded.FirstName,
                    LastName = userToBeAdded.LastName,
                    UserName = userToBeAdded.UserName,
                    JoinedDate = userToBeAdded.JoinedDate,
                    DateOfBirth = userToBeAdded.DateOfBirth,
                    Gender = userToBeAdded.Gender,
                    Location = userToBeAdded.Location,
                    StaffCode = userToBeAdded.StaffCode,
                    Type = existRole.Name,
                });
            }
            catch (Exception ex)
            {
                return Result<UserReadModel>.Failure(ex.InnerException.Message);
            }

        }
    }
}
