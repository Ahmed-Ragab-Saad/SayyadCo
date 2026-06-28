using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.Codes.Commands.GenerateCode;
using SayyadCo.Domain.Common;

namespace SayyadCo.API.Controllers
{
    [ApiController]
    [Authorize(Roles = $"{AppRoles.SuperAdmin},{AppRoles.Admin}")]
    public class AdminCodesController : CustomBaseController
    {
        /// <summary>Generate a code for SectionGame</summary>
        /// <remarks>
        /// Generates a unique code for a SectionGame with a specific role.
        /// Only Admins and SuperAdmins can generate codes.
        ///
        /// Sample request:
        ///
        ///     POST /api/Codes/generate-code
        ///     {
        ///         "sectionId": "3fa85f64...",
        ///         "gameId": "4gb96g75..."
        ///         "sectionGamePlanId": "neu7d3v4...",
        ///         "expiresAt": "2026-12-31T00:00:00Z"
        ///     }
        /// </remarks>
        /// <response code="200">Code generated successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">SectionGame or GameRole not found</response>
        [HttpPost("generate-code")]
        [ProducesResponseType(typeof(GenerateCodeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Generate([FromBody] GenerateCodeCommand command)
            => HandleResult(await Mediator.Send(command));
    }
}
