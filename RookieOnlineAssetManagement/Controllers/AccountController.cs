using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.ViewModels.User;

namespace RookieOnlineAssetManagement.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<Account>> GetCurrentUser()
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == UserId);
            if (user == null)
            {
                return Unauthorized();
            }
            var roles = await _userManager.GetRolesAsync(user);
            return CreateAccountObject(user, roles);
        }

        [Authorize(Policy = "IsDisable")]
        [HttpPut("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePassword)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == UserId);

            if (user == null)
            {
                return Unauthorized();
            }

            if (changePassword.CurrentPassword == "" && changePassword.NewPassword == "")
            {
                return BadRequest("Please fill in all field");
            }
            if (changePassword.CurrentPassword == "")
            {
                return BadRequest("Old password is required");
            }
            if (changePassword.NewPassword == "")
            {
                return BadRequest("New password is required");
            }
            if (changePassword.NewPassword.Length < 6)
            {
                return BadRequest("New password need at least 6 or more character");
            }



            // Check password
            var result = await _signInManager.CheckPasswordSignInAsync(user, changePassword.CurrentPassword, false);

            if (result.Succeeded && changePassword.NewPassword == changePassword.CurrentPassword)
            {
                return BadRequest("Your new password shouldn't match with the old one");
            }

            if (result.Succeeded)
            {
                var finaldResult = await _userManager.ChangePasswordAsync(user, changePassword.CurrentPassword, changePassword.NewPassword);
                if (finaldResult.Succeeded)
                {
                    try
                    {
                        user.IsDefaultPassword = false;
                        await _userManager.UpdateAsync(user);
                        var roles = await _userManager.GetRolesAsync(user);
                        return Ok(CreateAccountObject(user, roles));
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.InnerException.Message);
                    }

                }

                return BadRequest("Problem with reseting password");

            }

            return BadRequest("Incorrect password");
        }

        [Authorize(Policy = "IsDisable")]
        [HttpPut("resetpassword")]
        public async Task<IActionResult> ResetPassword(ChangePasswordModel changePassword)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == UserId);

            if (user == null)
            {
                return Unauthorized();
            }

            if (changePassword.NewPassword == "")
            {
                return BadRequest("New password is required");
            }

            if (changePassword.NewPassword.Length < 6)
            {
                return BadRequest("New password need at least 6 or more character");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, changePassword.NewPassword);

            if (result.Succeeded)
            {
                try
                {
                    user.IsDefaultPassword = false;
                    await _userManager.UpdateAsync(user);
                    var roles = await _userManager.GetRolesAsync(user);
                    return Ok(CreateAccountObject(user, roles));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.InnerException.Message);
                }
            }
            return BadRequest("Something went wrong");
        }

        [HttpGet("FirstLogin")]
        public async Task<IActionResult> FirstLogin()
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == UserId);

            if (user == null)
            {
                return Unauthorized();
            }
            try
            {
                user.IsDefaultPassword = false;
                await _userManager.UpdateAsync(user);
                var roles = await _userManager.GetRolesAsync(user);
                return Ok(CreateAccountObject(user, roles));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        [HttpPost("Logout")]
        public async Task<IActionResult> LogOut(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return Redirect("/");
            }
        }


        private Account CreateAccountObject(ApplicationUser user, IList<string> roles)
        {

            return new Account
            {
                Username = user.UserName,
                FullName = user.FullName,
                IsDisabled = user.IsDisabled,
                Role = roles.FirstOrDefault(),
                IsDefaultPassword = user.IsDefaultPassword ?? true
            };
        }

    }
}