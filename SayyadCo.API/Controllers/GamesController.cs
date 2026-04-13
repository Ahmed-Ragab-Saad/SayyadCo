using Microsoft.AspNetCore.Mvc;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.Games.Commands.CreateGame;

namespace SayyadCo.API.Controllers
{
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
        /// <response code="400">Validation error</response>
        /// <response code="404">GameType not found</response>
        [HttpPost]
        [ProducesResponseType(typeof(CreateGameResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreateGameCommand command)
            => HandleResult(await Mediator.Send(command));


    }
}
