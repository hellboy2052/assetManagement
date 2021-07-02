using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RookieOnlineAssetManagement.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        private string _userLocation;
        private string _userId;
        protected string UserLocation => _userLocation ??= HttpContext.User.FindFirst("Location").Value;
        protected string UserId => _userId ??= HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult HandleResult<T> (Result<T> result)
        {
            if (result == null)
            {
                return NotFound();
            }

            if (result.IsSuccess && typeof(T) == typeof(Task))
            {
                return NoContent();
            }
            if (result.IsSuccess && result.Value != null)
            {
                return Ok(result.Value);
            }
            if (result.IsSuccess && result.Value == null)
            {
                return NotFound();
            }
            return BadRequest(result.Error);
        }
    }
}