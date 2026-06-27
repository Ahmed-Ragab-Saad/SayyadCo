// SayyadCo.API/Controllers/AdminController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.Exams.Commands.ApproveExam;
using SayyadCo.Application.Features.Exams.Commands.ApproveExamUpdate;
using SayyadCo.Application.Features.Exams.Commands.RejectExam;
using SayyadCo.Application.Features.Exams.Commands.RejectExamUpdate;
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

        /// <summary>Approve an exam</summary>
        /// <remarks>
        /// Approves a specific exam.
        /// 
        /// This endpoint is accessible only by Admins and SuperAdmins.
        /// Once approved, the exam status is changed from "Pending" to "Approved"
        /// and it becomes visible to Students.
        /// 
        /// Sample request:
        /// 
        ///     POST /api/admin/exams/7fd45l90.../approve
        /// </remarks>
        /// <response code="200">Exam approved successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Exam not found</response>
        [HttpPost("exams/{examId}/approve")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Approve(string examId)
            => HandleResult(await Mediator.Send(new ApproveExamCommand
            {
                ExamId = examId
            }));

        /// <summary>Reject an exam</summary>
        /// <remarks>
        /// Rejects a specific exam.
        /// 
        /// This endpoint is accessible only by Admins and SuperAdmins.
        /// Once rejected, the exam status is changed from "Pending" to "Rejected"
        /// and it will not be visible to Students.
        /// 
        /// A rejection reason can be provided in the request body.
        /// 
        /// Sample request:
        /// 
        ///     POST /api/admin/exams/7fd45l90.../reject
        ///     {
        ///         "reason": "Invalid questions format"
        ///     }
        /// </remarks>
        /// <response code="200">Exam rejected successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Exam not found</response>
        [HttpPost("exams/{examId}/reject")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Reject(string examId, [FromBody] RejectExamCommand command)
        {
            command.ExamId = examId;
            return HandleResult(await Mediator.Send(command));
        }

        /// <summary>Approve exam update request</summary>
        /// <remarks>
        /// Approves a pending exam update request.
        /// 
        /// This endpoint is accessible only by Admins and SuperAdmins.
        /// Once approved, the requested changes are applied to the exam.
        /// 
        /// Sample request:
        /// 
        ///     POST /api/admin/exam-update-requests/9hf73k2a.../approve
        /// </remarks>
        /// <response code="200">Update request approved successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Update request not found</response>
        [HttpPost("exam-update-requests/{requestId}/approve")]
        [Authorize(Roles = $"{AppRoles.SuperAdmin},{AppRoles.Admin}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ApproveUpdate(string requestId)
            => HandleResult(await Mediator.Send(new ApproveExamUpdateCommand
            {
                RequestId = requestId
            }));

        /// <summary>Reject exam update request</summary>
        /// <remarks>
        /// Rejects a pending exam update request.
        /// 
        /// This endpoint is accessible only by Admins and SuperAdmins.
        /// Once rejected, the requested changes are discarded and not applied to the exam.
        /// 
        /// A rejection reason can be provided in the request body.
        /// 
        /// Sample request:
        /// 
        ///     POST /api/admin/exam-update-requests/9hf73k2a.../reject
        ///     {
        ///         "reason": "Invalid update data"
        ///     }
        /// </remarks>
        /// <response code="200">Update request rejected successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Update request not found</response>
        [HttpPost("exam-update-requests/{requestId}/reject")]
        [Authorize(Roles = $"{AppRoles.SuperAdmin},{AppRoles.Admin}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RejectUpdate(string requestId, [FromBody] RejectExamUpdateCommand command)
        {
            command.RequestId = requestId;
            return HandleResult(await Mediator.Send(command));
        }
    }
}