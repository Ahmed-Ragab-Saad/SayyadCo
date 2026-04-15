using MediatR;
using SayyadCo.Application.Common.DTOs;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.SectionGames.Commands.AddGameToFunnySection
{
    public class AddGameToFunnySectionCommandHandler : IRequestHandler<AddGameToFunnySectionCommand, Result<AddGameToSectionResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddGameToFunnySectionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AddGameToSectionResponseDto>> Handle(AddGameToFunnySectionCommand request, CancellationToken cancellationToken)
        {
            var funnySection = await _unitOfWork.Sections.GetFunnySection();
            if (funnySection is null)
                return Result<AddGameToSectionResponseDto>.NotFound("Funny section not found");

            var addedGames = new List<string>();
            var failedGames = new List<string>();

            foreach (var gameId in request.GamesIds)
            {
                var game = await _unitOfWork.Games.GetByIdAsync(gameId);
                if (game is null)
                {
                    failedGames.Add($"{gameId}: Not found");
                    continue;
                }

                var existing = await _unitOfWork.SectionGames.GetAsync(funnySection.Id, gameId);
                if (existing is not null)
                {
                    failedGames.Add($"{gameId}: Already exists");
                    continue;
                }

                var sectionGame = new SectionGame()
                {
                    SectionId = funnySection.Id,
                    GameId = gameId
                };

                await _unitOfWork.SectionGames.AddAsync(sectionGame);
                addedGames.Add(gameId);
            }

            await _unitOfWork.SaveChangesAsync();

            return Result<AddGameToSectionResponseDto>.Success(new AddGameToSectionResponseDto
            {
                Message = $"Games processed {(failedGames.Count > 0 ? $"but with {failedGames.Count} fails" : "")}",
                AddedGames = addedGames,
                FailedGames = failedGames
            });
        }
    }
}
