using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SayyadCo.Application.Features.Profiles;

namespace SayyadCo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfilesController : CustomBaseController
    {
        /// <summary>Get profile info</summary>
        /// <remarks>
        /// Returns the current user's profile information.
        ///
        /// Sample request:
        ///
        ///     GET /api/profiles/info
        /// </remarks>
        /// <response code="200">Returns profile info</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet("info")]
        [ProducesResponseType(typeof(GetProfileInfoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetInfo()
            => HandleResult(await Mediator.Send(new GetProfileInfoQuery()));
    }
}
