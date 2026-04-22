using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.AcademicYears.Commands.AddAcademicYear;
using SayyadCo.Application.Features.AcademicYears.Commands.DeleteAcademicYear;
using SayyadCo.Application.Features.AcademicYears.Commands.UpdateAcademicYear;
using SayyadCo.Application.Features.AcademicYears.Queries.GetAllAcademicYears;
using SayyadCo.Application.Features.Games.Queries.GetAllGames;
using SayyadCo.Domain.Common;

namespace SayyadCo.API.Controllers
{
    [Route("api/academic-years")]
    [Authorize]
    public class AcademicYearController : CustomBaseController
    {
        /// <summary>
        /// Create a new academic year
        /// </summary>
        /// <remarks>
        /// Creates a new academic year with Arabic and English titles.
        /// Only accessible by SuperAdmin or Admin.
        ///
        /// Sample request:
        ///
        ///     POST /api/academic-years
        ///     {
        ///         "titleAr": "الصف الأول الثانوي",
        ///         "titleEn": "First Secondary Grade"
        ///     }
        /// </remarks>
        /// <response code="200">Academic year created successfully</response>
        /// <response code="400">Validation error OR An academic year with the same Arabic or English title already exists</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        [HttpPost]
        [Authorize(Roles = $"{AppRoles.SuperAdmin},{AppRoles.Admin}")]
        [ProducesResponseType(typeof(AddAcademicYearResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create([FromBody] AddAcademicYearCommand command)
            => HandleResult(await Mediator.Send(command));

        /// <summary>
        /// Update an academic year
        /// </summary>
        /// <remarks>
        /// Update an existing academic year.
        /// Only accessible by SuperAdmin or Admin.
        ///
        /// Sample request:
        ///
        ///     PUT /api/academic-years/3y28gn19...
        ///     {
        ///         "titleAr": "الصف الأول الثانوي",
        ///         "titleEn": "First Secondary Grade"
        ///     }
        /// </remarks>
        /// <response code="200">Academic year updated successfully</response>
        /// <response code="400">Validation error OR An academic year with the same Arabic or English title already exists</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Academic year not found</response>
        [HttpPut("{id}")]
        [Authorize(Roles = $"{AppRoles.SuperAdmin},{AppRoles.Admin}")]
        [ProducesResponseType(typeof(AddAcademicYearResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateAcademicYearCommand command)
        {
            command.Id = id;
            return HandleResult(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete an academic year
        /// </summary>
        /// <remarks>
        /// Delete an existing academic year.
        /// Only accessible by SuperAdmin or Admin.
        ///
        /// Sample request:
        ///
        ///     DELETE /api/academic-years/3y28gn19...
        /// </remarks>
        /// <response code="200">Academic year deleted successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Academic year not found</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{AppRoles.SuperAdmin},{AppRoles.Admin}")]
        [ProducesResponseType(typeof(AddAcademicYearResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
            => HandleResult(await Mediator.Send(new DeleteAcademicYearCommand()
            {
                Id = id
            }));

        /// <summary>Get all academic years</summary>
        /// <remarks>
        /// Returns a paginated list of all academic years. Supports search and sorting.
        /// Only for Super Admin and Admin
        ///
        /// Sample request:
        ///
        ///     GET /api/academic-years?pageNumber=1&amp;pageSize=10&amp;searchTerm=first&amp;orderBy=titleEn&amp;isDescending=false
        /// </remarks>
        /// <response code="200">Returns paginated list of games</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        [HttpGet]
        [Authorize(Roles = $"{AppRoles.SuperAdmin},{AppRoles.Admin}")]
        [ProducesResponseType(typeof(PagedResult<GetAllGamesResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllAcademicYearsQuery query)
            => HandleResult(await Mediator.Send(query));
    }
}
