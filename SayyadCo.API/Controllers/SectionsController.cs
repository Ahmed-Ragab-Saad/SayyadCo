using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.Sections.Commands.CreateSection;
using SayyadCo.Application.Features.Sections.Commands.DeleteSection;
using SayyadCo.Application.Features.Sections.Commands.UpdateSection;
using SayyadCo.Application.Features.Sections.Queries.GetAllSections;
using SayyadCo.Application.Features.Sections.Queries.GetSectionById;
using SayyadCo.Domain.Common;

namespace SayyadCo.API.Controllers
{
    //[Authorize(Roles = AppRoles.SuperAdmin)]
    public class SectionsController : CustomBaseController
    {
        /// <summary>
        /// Create a new section
        /// </summary>
        /// <remarks>
        /// Creates a new section with Arabic and English titles, descriptions, and an image URL.
        /// Only accessible by SuperAdmin.
        ///
        /// Sample request:
        ///
        ///     POST /api/sections
        ///     {
        ///         "titleAr": "الرياضيات",
        ///         "titleEn": "Mathematics",
        ///         "descriptionAr": "قسم الرياضيات",
        ///         "descriptionEn": "Mathematics Section",
        ///         "image": "https://res.cloudinary.com/sayyadco/image/upload/math.jpg"
        ///     }
        /// </remarks>
        /// <response code="200">Section created successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost]
        [ProducesResponseType(typeof(CreateSectionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] CreateSectionCommand command)
            => HandleResult(await Mediator.Send(command));

        /// <summary>
        /// Update an existing section
        /// </summary>
        /// <remarks>
        /// Updates section data by ID. Only accessible by SuperAdmin.
        ///
        /// Sample request:
        ///
        ///     PUT /api/sections/3fa85f64-5717-4562-b3fc-2c963f66afa6
        ///     {
        ///         "titleAr": "الرياضيات المتقدمة",
        ///         "titleEn": "Advanced Mathematics",
        ///         "descriptionAr": "قسم الرياضيات المتقدمة",
        ///         "descriptionEn": "Advanced Mathematics Section",
        ///         "image": "https://res.cloudinary.com/sayyadco/image/upload/math2.jpg"
        ///     }
        /// </remarks>
        /// <response code="200">Section updated successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Section not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateSectionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateSectionCommand command)
        {
            command.Id = id;
            return HandleResult(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete a section
        /// </summary>
        /// <remarks>
        /// Deletes a section by ID. Only accessible by SuperAdmin.
        ///
        /// **Warning:** Deleting a section will also delete all associated games and questions.
        ///
        /// Sample request:
        ///
        ///     DELETE /api/sections/3fa85f64-5717-4562-b3fc-2c963f66afa6
        /// </remarks>
        /// <response code="200">Section deleted successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Section not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
            => HandleResult(await Mediator.Send(new DeleteSectionCommand { Id = id }));

        /// <summary>
        /// Get section by ID
        /// </summary>
        /// <remarks>
        /// Returns a single section details by its ID.
        ///
        /// Sample request:
        ///
        ///     GET /api/sections/3fa85f64-5717-4562-b3fc-2c963f66afa6
        /// </remarks>
        /// <response code="200">Returns section details</response>
        /// <response code="404">Section not found</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(GetSectionByIdResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
            => HandleResult(await Mediator.Send(new GetSectionByIdQuery { Id = id }));

        /// <summary>
        /// Get all sections
        /// </summary>
        /// <remarks>
        /// Returns a paginated list of all sections. Supports search and sorting.
        ///
        /// Sample request:
        ///
        ///     GET /api/sections?pageNumber=1&amp;pageSize=10&amp;searchTerm=math&amp;orderBy=titleEn&amp;isDescending=false
        /// </remarks>
        /// <response code="200">Returns paginated list of sections</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<GetAllSectionsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllSectionsQuery query)
            => HandleResult(await Mediator.Send(query));
    }
}
