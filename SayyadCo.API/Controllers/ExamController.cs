using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.Exams.Commands.CreateExam;
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
        /// <response code="400">Validation error</response>
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
            command.GroupId = groupId;
            command.AcademicYearId = academicYearId;
            command.Semester = semester;
            return HandleResult(await Mediator.Send(command));
        }
    }
}
