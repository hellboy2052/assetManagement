using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RookieOnlineAssetManagement.Services.CategoryService.Query;
using RookieOnlineAssetManagement.Services.CategoryService.Command;
using RookieOnlineAssetManagement.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;

namespace RookieOnlineAssetManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryReadModel>>> GetCategories()
        {
            return HandleResult(await Mediator.Send(new GetAllCategoriesQuery()));
        }

        [Authorize(Policy = "IsDisable")]
        [HttpPost]
        public async Task<ActionResult<CategoryReadModel>> PostCategory([FromForm] CategoryCreateModel createModel)
        {
            return HandleResult(await Mediator.Send(new CreateCategoriesCommand { CreateModel = createModel }));
        }

        [Authorize(Policy = "IsDisable")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            return HandleResult(await Mediator.Send(new DeleteCategoriesCommand { Id = id }));
        }
    }
}
