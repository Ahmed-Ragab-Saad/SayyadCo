using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SayyadCo.Application.Common.DTOs;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.Profiles.Commands.UpdateProfileInfo;
using SayyadCo.Application.Features.Profiles.Queries.GetMyFunnyGames;
using SayyadCo.Application.Features.Profiles.Queries.GetMyStudentGames;
using SayyadCo.Application.Features.Profiles.Queries.GetMyTeacherGames;
using SayyadCo.Application.Features.Profiles.Queries.GetProfileInfo;
using SayyadCo.Domain.Common;

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

        /// <summary>Update profile info</summary>
        /// <remarks>
        /// Updates the current user's profile information.
        ///
        /// Sample request:
        ///
        ///     PUT /api/profiles/info
        ///     {
        ///         "firstName": "Ahmed",
        ///         "lastName": "Mohamed",
        ///         "image": "https://res.cloudinary.com/sayyadco/image/upload/profile.jpg"
        ///     }
        /// </remarks>
        /// <response code="200">Profile updated successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401">Unauthorized</response>
        [HttpPut("info")]
        [ProducesResponseType(typeof(UpdateProfileInfoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateInfo([FromBody] UpdateProfileInfoCommand command)
            => HandleResult(await Mediator.Send(command));

        /// <summary>Get my teacher games</summary>
        /// <remarks>
        /// Returns all games the current user is subscribed to as a Teacher.
        ///
        /// Sample request:
        ///
        ///     GET /api/profiles/my-teacher-games?pageNumber=1&amp;pageSize=10
        /// </remarks>
        /// <response code="200">Returns paginated list of teacher games</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet("my-teacher-games")]
        [ProducesResponseType(typeof(PagedResult<MyGameResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMyTeacherGames([FromQuery] GetMyTeacherGamesQuery query)
            => HandleResult(await Mediator.Send(query));

        /// <summary>Get my student games</summary>
        /// <remarks>
        /// Returns all games the current user is subscribed to as a Student.
        ///
        /// Sample request:
        ///
        ///     GET /api/profiles/my-student-games?pageNumber=1&amp;pageSize=10
        /// </remarks>
        /// <response code="200">Returns paginated list of student games</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet("my-student-games")]
        [ProducesResponseType(typeof(PagedResult<MyGameResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMyStudentGames([FromQuery] GetMyStudentGamesQuery query)
            => HandleResult(await Mediator.Send(query));

        /// <summary>Get my funny games</summary>
        /// <remarks>
        /// Returns all funny games the current user is subscribed to (as Teacher or Student).
        ///
        /// Sample request:
        ///
        ///     GET /api/profiles/my-funny-games?pageNumber=1&amp;pageSize=10
        /// </remarks>
        /// <response code="200">Returns paginated list of funny games</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet("my-funny-games")]
        [ProducesResponseType(typeof(PagedResult<MyFunnyGameResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMyFunnyGames([FromQuery] GetMyFunnyGamesQuery query)
            => HandleResult(await Mediator.Send(query));
    }
}
