using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RookieOnlineAssetManagement.Services.AssignmentService.Command;
using RookieOnlineAssetManagement.Services.AssignmentService.Query;
using RookieOnlineAssetManagement.ViewModels.Asset;
using RookieOnlineAssetManagement.ViewModels.Assignment;
using RookieOnlineAssetManagement.ViewModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Controllers
{
    [Authorize]
    public class AssignmentsController : BaseController
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<AssignmentReadViewModel>> GetFiltered([FromQuery] AssignmentFilterModel filterModel)
        {
            return HandleResult(await Mediator.Send(new GetFilteredAssignmentsQuery { FilterModel = filterModel }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssignmentReadViewModel>> GetById(string id)
        {
            return HandleResult(await Mediator.Send(new GetAssignmentByIdQuery { Id = id }));
        }

        [HttpGet("edit/{id}")]
        public async Task<ActionResult<AssignmentReadViewModel>> GetEditDetailById(string id)
        {
            return HandleResult(await Mediator.Send(new GetAssignmentEditDetailByIdQuery { Id = id }));
        }

        [HttpGet("user/")]
        public async Task<ActionResult<IEnumerable<UserAssignmentReadModel>>> GetLoggedInUserAssignments()
        {
            return HandleResult(await Mediator.Send(new GetUserAssignmentsQuery { UserId = UserId }));
        }

        [Authorize(Policy = "IsDisable")]
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<AssignmentReadDetailModel>> PutAssignment(string id, [FromForm] AssignmentUpdateModel updateModel)
        {
            if (id != updateModel.AssignmentId)
            {
                return BadRequest();
            }
            return HandleResult(await Mediator.Send(new UpdateAssignmentCommand { AdminId = UserId, UpdateModel = updateModel }));
        }

        [Authorize(Policy = "IsDisable")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<AssignmentReadModel>> PostAssignment([FromForm] AssignmentCreateModel createModel)
        {
            var createdAssignment = await Mediator.Send(new CreateAssignmentCommand { AdminId = UserId, CreateModel = createModel });
            if (!createdAssignment.IsSuccess)
                return HandleResult(createdAssignment);
            return CreatedAtAction(nameof(GetById), new { id = createdAssignment.Value.AssignmentId }, createdAssignment.Value);
        }

        [Authorize(Policy = "IsDisable")]
        [HttpPost("state")]
        public async Task<IActionResult> PostAssignmentState(AssignmentStateUpdateModel stateUpdateModel)
        {
            return HandleResult(await Mediator.Send(new UpdateAssignmentStateCommand { StateUpdateModel = stateUpdateModel }));
        }

        [Authorize(Policy = "IsDisable")]
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(string id)
        {
            return HandleResult(await Mediator.Send(new DeleteAssignmentCommand { Id = id }));
        }
    }
}
