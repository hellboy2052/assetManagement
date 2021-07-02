using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RookieOnlineAssetManagement.Services.ReturningService.Command;
using RookieOnlineAssetManagement.Services.ReturningService.Query;
using RookieOnlineAssetManagement.ViewModels.Return;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Controllers
{
    [Authorize]
    public class ReturningsController : BaseController
    {
        [Authorize(Policy = "IsDisable")]
        [HttpPost]
        public async Task<ActionResult<ReturnReadModel>> PostRetuning(ReturnCreateModel createModel)
        {
            return HandleResult(await Mediator.Send(new CreateReturnCommand { UserId = UserId, CreateModel = createModel }));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReturnReadModel>>> GetAllReturning()
        {
            return HandleResult(await Mediator.Send(new GetAllReturnsQuery()));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("filter")]
        public async Task<ActionResult<ReturnReadModel>> GetFiltered([FromQuery] ReturnFilteredModel filterModel)
        {
            return HandleResult(await Mediator.Send(new GetFilteredReturningQuery { FilterModel = filterModel }));
        }

        [Authorize(Policy = "IsDisable")]
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReturning(string id)
        {
            return HandleResult(await Mediator.Send(new DeleteReturnCommand { Id = id }));
        }

        [Authorize(Policy = "IsDisable")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("state")]
        public async Task<ActionResult<ReturnReadModel>> PostStateReturning(ReturnStateUpdateModel stateUpdateModel)
        {
            return HandleResult(await Mediator.Send(new UpdateReturnStateCommand { UserId = UserId, returnStateUpdateModel = stateUpdateModel }));
        }
    }
}
