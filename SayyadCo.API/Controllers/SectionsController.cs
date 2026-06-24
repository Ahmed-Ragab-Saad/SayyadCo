using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SayyadCo.Application.Common.DTOs;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.SectionGames.Commands.AddAcademicYear;
using SayyadCo.Application.Features.SectionGames.Commands.AddGameToFunnySection;
using SayyadCo.Application.Features.SectionGames.Commands.AddGameToSection;
using SayyadCo.Application.Features.SectionGames.Commands.AssignPlans;
using SayyadCo.Application.Features.SectionGames.Commands.RemoveGameFromSection;
using SayyadCo.Application.Features.SectionGames.Queries.GetFunnySectionGames;
using SayyadCo.Application.Features.SectionGames.Queries.GetSectionGamePlans;
using SayyadCo.Application.Features.SectionGames.Queries.GetSectionGames;
using SayyadCo.Application.Features.Sections.Commands.CreateSection;
using SayyadCo.Application.Features.Sections.Commands.DeleteSection;
using SayyadCo.Application.Features.Sections.Commands.UpdateSection;
using SayyadCo.Application.Features.Sections.Queries.GetAllSections;
using SayyadCo.Application.Features.Sections.Queries.GetSectionById;
using SayyadCo.Domain.Common;

namespace SayyadCo.API.Controllers
{
    [Authorize(Roles = AppRoles.SuperAdmin)]
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
        /// <response code="401">Unauthorized</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagedResult<GetAllSectionsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllSectionsQuery query)
            => HandleResult(await Mediator.Send(query));

        /// <summary>Add games to section</summary>
        /// <remarks>
        /// Links existing games to a section.
        ///
        /// Sample request:
        ///
        ///     POST /api/sections/3fa85f64.../games
        ///     {
        ///         "gamesIds": [
        ///             "4gb96g75...",
        ///             "6fggin36...",
        ///             "mo63u8v1..."
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="200">Game added to section successfully</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost("{sectionId}/games")]
        [ProducesResponseType(typeof(AddGameToSectionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddGame(string sectionId, AddGameToSectionCommand request)
        {
            request.SectionId = sectionId;
            return HandleResult(await Mediator.Send(request));
        }

        /// <summary>Add academic year to a section game</summary>
        /// <remarks>
        /// Links existing academic year to a section game.
        ///
        /// Sample request:
        ///
        ///     POST /api/sections/3fa85f64.../games/d7ak37c1.../academic-years/8avz5d9a...
        /// </remarks>
        /// <response code="200">Game added to section successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Section, game, or academic year not found</response>
        [HttpPost("{sectionId}/games/{gameId}/academic-years/{academicYearId}")]
        [ProducesResponseType(typeof(AddGameToSectionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddacademicYearToGame(string sectionId, string gameId, string academicYearId)
            => HandleResult(await Mediator.Send(new AddAcademicYearToSectionGameCommand()
            {
                SectionId = sectionId,
                GameId = gameId,
                AcademicYearId = academicYearId
            }));

        /// <summary>Add games to funny section</summary>
        /// <remarks>
        /// Links existing games to a funny section.
        ///
        /// Sample request:
        ///
        ///     POST /api/sections/funny/games
        ///     {
        ///         "gamesIds": [
        ///             "4gb96g75...",
        ///             "6fggin36...",
        ///             "mo63u8v1..."
        ///         ]
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Game added to section successfully</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost("funny/games")]
        [ProducesResponseType(typeof(AddGameToSectionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddGameToFunnySection(AddGameToFunnySectionCommand request)
            => HandleResult(await Mediator.Send(request));

        /// <summary>Remove game from section</summary>
        /// <remarks>
        /// Removes a game from a section.
        ///
        /// **Warning:** This will also delete all associated questions and codes.
        ///
        /// Sample request:
        ///
        ///     DELETE /api/sections/3fa85f64.../games/4gb96g75...
        /// </remarks>
        /// <response code="200">Game removed from section successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Game not found in section</response>
        [HttpDelete("{sectionId}/games/{gameId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveGame(string sectionId, string gameId)
            => HandleResult(await Mediator.Send(new RemoveGameFromSectionCommand
            {
                SectionId = sectionId,
                GameId = gameId
            }));

        /// <summary>Get all games in a section</summary>
        /// <remarks>
        /// Returns all games linked to a specific section.
        ///
        /// Sample request:
        ///
        ///     GET /api/sections/3fa85f64.../games
        /// </remarks>
        /// <response code="200">Returns list of games in section</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Section not found</response>
        [HttpGet("{sectionId}/games")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagedResult<GetSectionGamesResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGames([FromRoute] string sectionId)
            => HandleResult(await Mediator.Send(new GetSectionGamesQuery()
            {
                SectionId = sectionId
            }));

        /// <summary>Get all games in a funny section</summary>
        /// <remarks>
        /// Returns all games linked to a funny section.
        ///
        /// Sample request:
        ///
        ///     GET /api/sections/funny/games
        /// </remarks>
        /// <response code="200">Returns list of games in section</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet("funny/games")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagedResult<GetSectionGamesResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GatFunnySectionGames([FromQuery] GetFunnySectionGamesQuery query)
            => HandleResult(await Mediator.Send(query));

        /// <summary>Assign plans to section game</summary>
        /// <remarks>
        /// Assigns one or more plans to a SectionGame with a specific type (Teacher or Student).
        /// Only Admins and SuperAdmins can assign plans.
        ///
        /// Sample request:
        ///
        ///     POST /api/sections/3fa85f64.../games/4gb96g75.../plans
        ///     {
        ///         "plans": [
        ///             {
        ///                 "planId": "5cd12h89...",
        ///                 "planType": 0
        ///             },
        ///             {
        ///                 "planId": "5cd12h89...",
        ///                 "planType": 1
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="200">Plans assigned successfully</response>
        /// <response code="400">Validation error or plan already assigned</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">SectionGame or Plan not found</response>
        [HttpPost("{sectionId}/games/{gameId}/plans")]
        [Authorize(Roles = $"{AppRoles.SuperAdmin},{AppRoles.Admin}")]
        [ProducesResponseType(typeof(List<AssignPlansResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AssignPlans(string sectionId, string gameId,
            [FromBody] AssignPlansToSectionGameCommand command)
        {
            command.SectionId = sectionId;
            command.GameId = gameId;
            return HandleResult(await Mediator.Send(command));
        }

        /// <summary>Get plans assigned to a section game</summary>
        /// <remarks>
        /// Retrieves all plans assigned to a specific SectionGame,
        /// including the plan details and assignment type
        /// (Teacher or Student).
        ///
        /// This endpoint can be accessed anonymously and is commonly
        /// used to display the available plans associated with a game
        /// inside a section.
        ///
        /// Sample request:
        ///
        ///     GET /api/sections/3fa85f64.../games/4gb96g75.../plans
        ///
        /// Sample response:
        ///
        ///     [
        ///         {
        ///             "planId": "5cd12h89...",
        ///             "planName": "Basic Plan",
        ///             "gameRoleId": "2uv495g7..."
        ///         },
        ///         {
        ///             "planId": "7ef34k21...",
        ///             "planName": "Premium Plan",
        ///             "gameRoleId": "be42f69b..."
        ///         }
        ///     ]
        /// </remarks>
        /// <response code="200">Plans retrieved successfully</response>
        /// <response code="404">SectionGame not found</response>
        [HttpGet("{sectionId}/games/{gameId}/plans")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<GetSectionGamePlansResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPlans(string sectionId, string gameId)
            => HandleResult(await Mediator.Send(new GetSectionGamePlansQuery
            {
                SectionId = sectionId,
                GameId = gameId
            }));
    }
}
