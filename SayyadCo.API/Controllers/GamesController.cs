using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.Games.Commands.CreateGame;
using SayyadCo.Application.Features.Games.Commands.DeleteGame;
using SayyadCo.Application.Features.Games.Commands.UpdateGame;
using SayyadCo.Application.Features.Games.Queries.GetAllGames;
using SayyadCo.Application.Features.Games.Queries.GetGameById;
using SayyadCo.Domain.Common;

namespace SayyadCo.API.Controllers
{
    [Authorize(Roles = AppRoles.SuperAdmin)]
    public class GamesController : CustomBaseController
    {
        /// <summary>Create a new game</summary>
        /// <remarks>
        /// Creates a new game with Arabic and English titles, descriptions, and image URL.
        ///
        /// Sample request:
        ///
        ///     POST /api/games
        ///     {
        ///         "titleAr": "من سيربح المليون",
        ///         "titleEn": "Who Wants to Be a Millionaire",
        ///         "descriptionAr": "لعبة الأسئلة والأجوبة",
        ///         "descriptionEn": "Questions and Answers Game",
        ///         "image": "https://res.cloudinary.com/sayyadco/image/upload/game.jpg"
        ///     }
        /// </remarks>
        /// <response code="200">Game created successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="400">Validation error</response>
        [HttpPost]
        [ProducesResponseType(typeof(CreateGameResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreateGameCommand command)
            => HandleResult(await Mediator.Send(command));

        /// <summary>Update an existing game</summary>
        /// <remarks>
        /// Updates game data by ID.
        ///
        /// Sample request:
        ///
        ///     PUT /api/games/3fa85f64-5717-4562-b3fc-2c963f66afa6
        ///     {
        ///         "titleAr": "من سيربح المليون المتقدم",
        ///         "titleEn": "Advanced Millionaire",
        ///         "descriptionAr": "نسخة متقدمة",
        ///         "descriptionEn": "Advanced version",
        ///         "image": "https://res.cloudinary.com/sayyadco/image/upload/game2.jpg",
        ///         "gameTypeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///     }
        /// </remarks>
        /// <response code="200">Game updated successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Game not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateGameResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]

        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateGameCommand command)
        {
            command.Id = id;
            return HandleResult(await Mediator.Send(command));
        }

        /// <summary>Delete a game</summary>
        /// <remarks>
        /// Deletes a game by ID.
        ///
        /// **Warning:** Deleting a game will also delete all associated section games and questions.
        ///
        /// Sample request:
        ///
        ///     DELETE /api/games/3fa85f64-5717-4562-b3fc-2c963f66afa6
        /// </remarks>
        /// <response code="200">Game deleted successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Game not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
            => HandleResult(await Mediator.Send(new DeleteGameCommand { Id = id }));

        /// <summary>Get all games</summary>
        /// <remarks>
        /// Returns a paginated list of all games. Supports search and sorting.
        ///
        /// Sample request:
        ///
        ///     GET /api/games?pageNumber=1&amp;pageSize=10&amp;searchTerm=million&amp;orderBy=titleEn&amp;isDescending=false
        /// </remarks>
        /// <response code="200">Returns paginated list of games</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagedResult<GetAllGamesResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllGamesQuery query)
            => HandleResult(await Mediator.Send(query));

        /// <summary>Get game by ID</summary>
        /// <remarks>
        /// Returns a single game details including its game type.
        ///
        /// Sample request:
        ///
        ///     GET /api/games/3fa85f64-5717-4562-b3fc-2c963f66afa6
        /// </remarks>
        /// <response code="200">Returns game details</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Game not found</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(GetGameByIdResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
            => HandleResult(await Mediator.Send(new GetGameByIdQuery { Id = id }));
    }
}
