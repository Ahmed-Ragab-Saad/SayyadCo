using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.Exams.Commands.ApproveExam;
using SayyadCo.Application.Features.Exams.Commands.ApproveExamUpdate;
using SayyadCo.Application.Features.Exams.Commands.CreateExam;
using SayyadCo.Application.Features.Exams.Commands.DeleteExam;
using SayyadCo.Application.Features.Exams.Commands.RejectExam;
using SayyadCo.Application.Features.Exams.Commands.RejectExamUpdate;
using SayyadCo.Application.Features.Exams.Commands.UpdateExam;
using SayyadCo.Application.Features.Exams.Queries.GetExamById;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Enums;

namespace SayyadCo.API.Controllers
{
    [Route("api/sections/{sectionId}/games/{gameId}/academic-years/{academicYearId}/semesters/{semester}/groups/{groupId}")]
    [ApiController]
    [Authorize]
    public class ExamController : CustomBaseController
    {
        /// <summary>Create a new exam</summary>
        /// <remarks>
        /// Creates a new exam inside a Group.
        /// Teachers create exams with Pending status waiting for Admin approval.
        /// Admins and SuperAdmins create exams with Approved status directly.
        ///
        /// Sample request:
        ///
        ///     POST /api/sections/3fa85f64.../games/4gb96g75.../academic-years/5cd12h89.../semesters/1/groups/8hj23k56.../exams
        ///     {
        ///         "titleAr": "امتحان الرياضيات",
        ///         "titleEn": "Math Exam",
        ///         "descriptionAr": "امتحان في الرياضيات",
        ///         "descriptionEn": "Mathematics Exam",
        ///         "questions": [
        ///             {
        ///                 "contentJson": "{\"question\":\"كام 2+2؟\",\"options\":[\"3\",\"4\",\"5\"],\"correctIndex\":1}",
        ///                 "points": 10
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="200">Exam created successfully</response>
        /// <response code="400">Validation error or Group does not belong to the specified section, game, academic year, or semester</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Group or AcademicYear not found</response>
        [HttpPost("exams")]
        [ProducesResponseType(typeof(CreateExamResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create(string sectionId, string gameId, string groupId, string academicYearId,
            Semester semester, [FromBody] CreateExamCommand command)
        {
            command.SectionId = sectionId;
            command.GameId = gameId;
            command.AcademicYearId = academicYearId;
            command.Semester = semester;
            command.GroupId = groupId;
            return HandleResult(await Mediator.Send(command));
        }

        /// <summary>Approve an exam</summary>
        /// <remarks>
        /// Approves a specific exam داخل Group.
        /// 
        /// This endpoint is accessible only by Admins and SuperAdmins.
        /// Once approved, the exam status is changed from "Pending" to "Approved"
        /// and it becomes visible to Students.
        /// 
        /// Sample request:
        /// 
        ///     POST /api/exams/7fd45l90.../approve
        /// </remarks>
        /// <response code="200">Exam approved successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Exam not found</response>
        [HttpPost("/api/exams/{examId}/approve")]
        [Authorize(Roles = $"{AppRoles.SuperAdmin},{AppRoles.Admin}")]
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
        /// Rejects a specific exam داخل Group.
        /// 
        /// This endpoint is accessible only by Admins and SuperAdmins.
        /// Once rejected, the exam status is changed from "Pending" to "Rejected"
        /// and it will not be visible to Students.
        /// 
        /// A rejection reason can be provided in the request body.
        /// 
        /// Sample request:
        /// 
        ///     POST /api/exams/7fd45l90.../reject
        ///     {
        ///         "reason": "Invalid questions format"
        ///     }
        /// </remarks>
        /// <response code="200">Exam rejected successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Exam not found</response>
        [HttpPost("/api/exams/{examId}/reject")]
        [Authorize(Roles = $"{AppRoles.SuperAdmin},{AppRoles.Admin}")]
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

        /// <summary>Delete an exam</summary>
        /// <remarks>
        /// Deletes a specific exam from a Group within a SectionGame.
        /// 
        /// Only the exam owner (Teacher) or Admins/SuperAdmins can delete the exam.
        /// This action is irreversible and will permanently remove the exam and its related data.
        /// 
        /// Sample request:
        /// 
        ///     DELETE /api/sections/sy4c21m8.../games/4gb96g75.../academic-years/5cd12h89.../semesters/1/groups/8hj23k56.../exams/7fd45l90...
        /// </remarks>
        /// <response code="200">Exam deleted successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Exam not found</response>
        [HttpDelete("exams/{examId}")]
        [Authorize(Roles = $"{AppRoles.SuperAdmin},{AppRoles.Admin}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string sectionId, string gameId, string academicYearId, Semester semester,
            string groupId, string examId)
            => HandleResult(await Mediator.Send(new DeleteExamCommand
            {
                SectionId = sectionId,
                GameId = gameId,
                AcademicYearId = academicYearId,
                Semester = semester,
                GroupId = groupId,
                ExamId = examId
            }));

        /// <summary>Get exam by ID</summary>
        /// <remarks>
        /// Retrieves a specific exam from a Group within a SectionGame.
        /// 
        /// Returns full exam details including its questions.
        /// Only approved exams are visible to Students, while Admins and Teachers
        /// may access exams regardless of their status based on permissions.
        /// 
        /// Sample request:
        /// 
        ///     GET /api/sections/sy4c21m8.../games/4gb96g75.../academic-years/5cd12h89.../semesters/1/groups/8hj23k56.../exams/7fd45l90...
        /// </remarks>
        /// <response code="200">Exam retrieved successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Exam not found</response>
        [HttpGet("exams/{examId}")]
        [ProducesResponseType(typeof(GetExamByIdResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string sectionId, string gameId, string academicYearId, Semester semester,
            string groupId, string examId)
            => HandleResult(await Mediator.Send(new GetExamByIdQuery
            {
                SectionId = sectionId,
                GameId = gameId,
                AcademicYearId = academicYearId,
                Semester = semester,
                GroupId = groupId,
                ExamId = examId
            }));

        /// <summary>Update an exam</summary>
        /// <remarks>
        /// Updates an existing exam داخل Group.
        /// 
        /// Teachers can submit update requests for exams, which require Admin/SuperAdmin approval.
        /// Admins and SuperAdmins can update exams directly without approval.
        /// 
        /// Updated data may include title, description, and questions.
        /// 
        /// Sample request:
        /// 
        ///     PUT /api/sections/sy4c21m8.../games/4gb96g75.../academic-years/5cd12h89.../semesters/1/groups/8hj23k56.../exams/7fd45l90...
        ///     {
        ///         "titleAr": "Updated Arabic Title",
        ///         "titleEn": "Updated English Title",
        ///         "descriptionAr": "Updated description",
        ///         "descriptionEn": "Updated description",
        ///         "questions": [
        ///             {
        ///                 "contentJson": "{\"question\":\"2+2?\",\"options\":[\"3\",\"4\",\"5\"],\"correctIndex\":1}",
        ///                 "points": 10
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="200">Exam updated successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Exam not found</response>
        [HttpPut("exams/{examId}")]
        [ProducesResponseType(typeof(UpdateExamResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string sectionId, string gameId, string academicYearId, Semester semester,
            string groupId, string examId, [FromBody] UpdateExamCommand command)
        {
            command.SectionId = sectionId;
            command.GameId = gameId;
            command.AcademicYearId = academicYearId;
            command.Semester = semester;
            command.GroupId = groupId;
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
        ///     POST /api/exam-update-requests/9hf73k2a.../approve
        /// </remarks>
        /// <response code="200">Update request approved successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Update request not found</response>
        [HttpPost("/api/exam-update-requests/{requestId}/approve")]
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
        ///     POST /api/exam-update-requests/9hf73k2a.../reject
        ///     {
        ///         "reason": "Invalid update data"
        ///     }
        /// </remarks>
        /// <response code="200">Update request rejected successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Update request not found</response>
        [HttpPost("/api/exam-update-requests/{requestId}/reject")]
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
