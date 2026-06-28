using Microsoft.AspNetCore.Mvc;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.Codes.Commands.UseCode;

namespace SayyadCo.API.Controllers
{
    [Route("api/sections/{sectionId}/games/{gameId}/codes")]
    public class UserCodesController : CustomBaseController
    {
        /// <summary>Use a subscription code</summary>
        /// <remarks>
        /// Activates a subscription code to join a SectionGame as Teacher or Student.
        /// The subscription duration is determined by the plan associated with the code.
        ///
        /// Sample request:
        ///
        ///     POST /api/sections/anc72m9d.../games/jsv6ae39.../codes/use
        ///     {
        ///         "value": "ABC12345"
        ///     }
        /// </remarks>
        /// <response code="200">Code used successfully, user subscribed</response>
        /// <response code="400">Code already used, expired, user already subscribed, or this code does not belong to this game</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Code not found</response>
        [HttpPost("use")]
        [ProducesResponseType(typeof(UseCodeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UseCode(string sectionId, string gameId, [FromBody] UseCodeCommand command)
        {
            command.SectionId = sectionId;
            command.GameId = gameId;
            return HandleResult(await Mediator.Send(command));
        }
    }
}
