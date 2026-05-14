// SayyadCo.API/Controllers/AdminController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SayyadCo.Application.Features.Exams.Queries.GetPendingExams;
using SayyadCo.Application.Features.Exams.Queries.GetPendingExamUpdates;
using SayyadCo.Domain.Common;
namespace SayyadCo.API.Controllers
{
    [Authorize(Roles = $"{AppRoles.SuperAdmin},{AppRoles.Admin}")]
    public class AdminController : CustomBaseController
    {
        /// <summary>Get all pending exams</summary>
        /// <remarks>
        /// Returns a paginated list of all exams waiting for approval.
        ///
        /// Sample request:
        ///
        ///     GET /api/admin/pending-exams?pageNumber=1&amp;pageSize=10
        /// </remarks>
        /// <response code="200">Returns paginated list of pending exams</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        [HttpGet("pending-exams")]
        [ProducesResponseType(typeof(PagedResult<GetPendingExamsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetPendingExams([FromQuery] GetPendingExamsQuery query)
            => HandleResult(await Mediator.Send(query));

        /// <summary>Get all pending exam update requests</summary>
        /// <remarks>
        /// Returns a paginated list of all exam update requests waiting for approval.
        ///
        /// Sample request:
        ///
        ///     GET /api/admin/pending-exam-updates?pageNumber=1&amp;pageSize=10
        /// </remarks>
        /// <response code="200">Returns paginated list of pending exam update requests</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        [HttpGet("pending-exam-updates")]
        [ProducesResponseType(typeof(PagedResult<GetPendingExamUpdatesResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetPendingExamUpdates([FromQuery] GetPendingExamUpdatesQuery query)
            => HandleResult(await Mediator.Send(query));
    }
}