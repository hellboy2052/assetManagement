using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RookieOnlineAssetManagement.Services.UserService.Command;
using RookieOnlineAssetManagement.Services.UserService.Query;
using RookieOnlineAssetManagement.ViewModels.User;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<UserReadViewModel>> Get ([FromQuery] PaginationModel paginationModel)
        {

            return HandleResult(await Mediator.Send(new GetAllUsersQuery { Location = UserLocation, PaginationModel = paginationModel }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadViewModel>> GetById (string id)
        {
            return HandleResult(await Mediator.Send(new GetUserByIdQuery { Location = UserLocation, Id = id }));
        }
        [HttpGet("filter")]
        public async Task<ActionResult<UserReadViewModel>> GetFiltered ([FromQuery] UserFilterModel filterModel)
        {
            return HandleResult(await Mediator.Send(new GetFilteredUsersQuery { UserId = UserId, FilterModel = filterModel }));
        }

        [Authorize(Policy = "IsDisable")]
        [HttpPost]
        public async Task<ActionResult<UserReadModel>> Post ([FromForm] UserCreateModel createModel)
        {
            var createdUser = await Mediator.Send(new CreateUserCommand { Location = UserLocation, CreateModel = createModel });
            if (!createdUser.IsSuccess)
                return BadRequest();
            return CreatedAtAction(nameof(GetById), new { assetcode = createdUser.Value.Id }, createdUser.Value);
        }

        [Authorize(Policy = "IsDisable")]
        [HttpPut("{id}")]
        public async Task<ActionResult<UserReadModel>> Put (string id, [FromForm] UserUpdateModel updateModel)
        {
            if (id != updateModel.Id)
            {
                return BadRequest("Id doesn't match");
            }
            return HandleResult(await Mediator.Send(new UpdateUserCommand { Location = UserLocation, UpdateModel = updateModel }));
        }

        [Authorize(Policy = "IsDisable")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (string id)
        {
            return HandleResult(await Mediator.Send(new DeleteUserCommand { Id = id, Location = UserLocation }));
        }
    }
}
