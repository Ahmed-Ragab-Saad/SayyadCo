using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.Tests.Commands.CreateTest;
using SayyadCo.Application.Features.Tests.Commands.DeleteTest;
using SayyadCo.Application.Features.Tests.Commands.UpdateTest;
using SayyadCo.Application.Features.Tests.Queries.GetAllTests;
using SayyadCo.Application.Features.Tests.Queries.GetTestById;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Enums;

namespace SayyadCo.API.Controllers
{
    [Route("api/sections/{sectionId}/games/{gameId}")]
    [ApiController]
    [Authorize]
    public class TestController : CustomBaseController
    {
        /// <summary>Create a new test</summary>
        /// <remarks>
        /// Creates a new test inside a SectionGame.
        /// Only Teachers and Admins can create tests.
        ///
        /// Sample request:
        ///
        ///     POST api/sections/7gv11cne.../games/in19c4dj.../academic-years/an18xc40.../semester/1/tests
        ///     {
        ///         "titleAr": "اختبار الرياضيات",
        ///         "titleEn": "Math Test",
        ///         "descriptionAr": "اختبار في الرياضيات",
        ///         "descriptionEn": "Mathematics Test",
        ///         "questions": [
        ///             {
        ///                 "contentJson": "{\"question\":\"كام 2+2؟\",\"options\":[\"3\",\"4\",\"5\"],\"correctIndex\":1}",
        ///                 "points": 10
        ///             },
        ///             {
        ///                 "contentJson": "{\"question\":\"كام 5×5؟\",\"options\":[\"20\",\"25\",\"30\"],\"correctIndex\":1}",
        ///                 "points": 10
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="200">Test created successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">SectionGame or academic year not found</response>
        [HttpPost("academic-years/{academicYearId}/semester/{semester}/tests")]
        [Authorize(Roles = $"{AppRoles.SuperAdmin},{AppRoles.Admin}")]
        [ProducesResponseType(typeof(CreateTestResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create(string sectionId, string gameId, string academicYearId, Semester semester,
            [FromBody] CreateTestCommand command)
        {
            command.SectionId = sectionId;
            command.GameId = gameId;
            command.AcademicYearId = academicYearId;
            command.Semester = semester;
            return HandleResult(await Mediator.Send(command));
        }

        /// <summary>Update test</summary>
        /// <remarks>
        /// Update test.
        /// Only Super Admins and Admins and Teacher can update tests.
        ///
        /// Sample request:
        ///
        ///     PUT api/sections/7gv11cne.../games/in19c4dj.../academic-years/an18xc40.../semester/1/tests/3fa85f64...
        ///     {
        ///         "titleAr": "اختبار الرياضيات",
        ///         "titleEn": "Math Test",
        ///         "descriptionAr": "اختبار في الرياضيات",
        ///         "descriptionEn": "Mathematics Test",
        ///         "questions": [
        ///             {
        ///                 "contentJson": "{\"question\":\"كام 2+2؟\",\"options\":[\"3\",\"4\",\"5\"],\"correctIndex\":1}",
        ///                 "points": 10
        ///             },
        ///             {
        ///                 "contentJson": "{\"question\":\"كام 5×5؟\",\"options\":[\"20\",\"25\",\"30\"],\"correctIndex\":1}",
        ///                 "points": 10
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="200">Test updated successfully</response>
        /// <response code="400">Invalid academic year or semester</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Test not found</response>
        [HttpPut("academic-years/{academicYearId}/semester/{semester}/tests/{testId}")]
        [Authorize(Roles = $"{AppRoles.SuperAdmin},{AppRoles.Admin}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTest(string sectionId, string gameId, string academicYearId, Semester semester,
            string testId, [FromBody] UpdateTestCommand command)
        {
            command.SectionId = sectionId;
            command.GameId = gameId;
            command.TestId = testId;
            command.AcademicYearId = academicYearId;
            command.Semester = semester;
            return HandleResult(await Mediator.Send(command));
        }

        /// <summary>Delete test</summary>
        /// <remarks>
        /// Deletes a test and all its associated questions.
        /// Only Super Admins and Admins can delete tests.
        ///
        /// Sample request:
        ///
        ///     DELETE api/sections/7gv11cne.../games/in19c4dj.../academic-years/an18xc40.../semester/1/tests/7ab30t1q...
        /// </remarks>
        /// <response code="200">Test deleted successfully</response>
        /// <response code="400">Invalid academic year or semester</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Test not found</response>
        [HttpDelete("academic-years/{academicYearId}/semester/{semester}/tests/{testId}")]
        [Authorize(Roles = $"{AppRoles.SuperAdmin},{AppRoles.Admin}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string sectionId, string gameId, string academicYearId, Semester semester,
            string testId)
            => HandleResult(await Mediator.Send(new DeleteTestCommand
            {
                TestId = testId,
                SectionId = sectionId,
                GameId = gameId,
                AcademicYearId = academicYearId,
                Semester = semester
            }));


        /// <summary>Get all tests by academic year and semester</summary>
        /// <remarks>
        /// Returns a paginated list of tests filtered by academic year and semester.
        /// Accessible by Teachers, Students, Admins and SuperAdmins subscribed to this game.
        ///
        /// Sample request:
        ///
        ///     GET /api/sections/3fa85f64.../games/4gb96g75.../academic-years/5cd12h89.../semesters/1/tests?pageNumber=1&amp;pageSize=10&amp;searchTerm=math
        /// </remarks>
        /// <response code="200">Returns paginated list of tests</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden — not subscribed to this game</response>
        /// <response code="404">SectionGame or AcademicYear not found</response>
        [HttpGet("academic-years/{academicYearId}/semesters/{semester}/tests")]
        [ProducesResponseType(typeof(PagedResult<GetAllTestsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll(string sectionId, string gameId, string academicYearId, Semester semester,
            [FromQuery] GetAllTestsQuery query)
        {
            query.SectionId = sectionId;
            query.GameId = gameId;
            query.AcademicYearId = academicYearId;
            query.Semester = semester;
            return HandleResult(await Mediator.Send(query));
        }

        /// <summary>Get test by id</summary>
        /// <remarks>
        /// Retrieve a specific test with its questions.
        ///
        /// Allowed roles:
        /// - Super Admin
        /// - Admin
        /// - Teacher
        ///
        /// Sample request:
        ///
        ///     GET /api/sections/7gv11cne.../games/in19c4dj.../academic-years/an18xc40.../semester/1/tests/3fa85f64...
        ///
        /// </remarks>
        /// <response code="200">Test retrieved successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Test not found</response>
        [HttpGet("academic-years/{academicYearId}/semester/{semester}/tests/{testId}")]
        [ProducesResponseType(typeof(GetTestByIdResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTestById(string sectionId, string gameId, string testId)
        {
            return HandleResult(await Mediator.Send(new GetTestByIdQuery
            {
                TestId = testId,
                SectionId = sectionId,
                GameId = gameId
            }));
        }
    }
}
