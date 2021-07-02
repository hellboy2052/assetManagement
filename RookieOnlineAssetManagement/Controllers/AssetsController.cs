using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RookieOnlineAssetManagement.Services.AssetService.Command;
using RookieOnlineAssetManagement.Services.AssetService.Query;
using RookieOnlineAssetManagement.ViewModels.Asset;
using RookieOnlineAssetManagement.ViewModels.User;

namespace RookieOnlineAssetManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AssetsController : BaseController
    {
        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<AssetReadViewModel>> GetAssets([FromQuery] PaginationModel paginationModel)
        {
            return HandleResult(await Mediator.Send(new GetAllAssetsQuery { Location = UserLocation, PaginationModel = paginationModel }));
        }

        [HttpGet("{assetcode}")]
        public async Task<ActionResult<AssetDetailReadModel>> GetAssetByAssetCode(string assetcode)
        {
            return HandleResult(await Mediator.Send(new GetAssetDetailQuery { Location = UserLocation, AssetCode = assetcode }));
        }

        [HttpGet("filter")]
        public async Task<ActionResult<AssetReadViewModel>> GetFiltered([FromQuery] AssetFilterModel filterModel)
        {
            return HandleResult(await Mediator.Send(new GetFilteredAssetsQuery { Location = UserLocation, FilterModel = filterModel }));
        }

        [Authorize(Policy = "IsDisable")]
        [HttpPost]
        public async Task<ActionResult<AssetReadModel>> PostAssets([FromForm] AssetCreateModel createModel)
        {
            var createdAsset = await Mediator.Send(new CreateAssetCommand { Location = UserLocation, CreateModel = createModel });
            if (!createdAsset.IsSuccess)
                return HandleResult(createdAsset);
            return CreatedAtAction(nameof(GetAssetByAssetCode), new { assetcode = createdAsset.Value.AssetCode }, createdAsset.Value);
        }

        [Authorize(Policy = "IsDisable")]
        [HttpPut("{id}")]
        public async Task<ActionResult<AssetReadModel>> Put(string id, [FromForm] AssetUpdateModel updateModel)
        {
            if (id != updateModel.AssetCode)
            {
                return BadRequest("Id doesn't match");
            }

            return HandleResult(await Mediator.Send(new UpdateAssetCommand { Location = UserLocation, UpdateModel = updateModel }));
        }

        //DELETE: api/Categories/5
        [Authorize(Policy = "IsDisable")]
        [HttpDelete("{assetcode}")]
        public async Task<IActionResult> DeleteAsset(string assetcode)
        {
            return HandleResult(await Mediator.Send(new DeleteAssetCommand { AssetCode = assetcode, Location = UserLocation }));

        }
    }
}
