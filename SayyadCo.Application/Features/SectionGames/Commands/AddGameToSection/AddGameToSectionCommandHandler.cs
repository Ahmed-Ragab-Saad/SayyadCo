using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.SectionGames.Commands.AddGameToSection
{
    public class AddGameToSectionCommandHandler : IRequestHandler<AddGameToSectionCommand, Result<AddGameToSectionResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddGameToSectionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AddGameToSectionResponseDto>> Handle(AddGameToSectionCommand request, CancellationToken cancellationToken)
        {
            var section = await _unitOfWork.Sections.GetByIdAsync(request.SectionId);
            if (section is null)
                return Result<AddGameToSectionResponseDto>.NotFound("Section not found");

            var game = await _unitOfWork.Games.GetByIdAsync(request.GameId);
            if (game is null)
                return Result<AddGameToSectionResponseDto>.NotFound("Game not found");

            var existing = await _unitOfWork.SectionGames.GetAsync(request.SectionId, request.GameId);
            if (existing is not null)
                return Result<AddGameToSectionResponseDto>.Failure("Game already exists in this section");

            var sectionGame = new SectionGame()
            {
                SectionId = request.SectionId,
                GameId = request.GameId
            };

            await _unitOfWork.SectionGames.AddAsync(sectionGame);
            await _unitOfWork.SaveChangesAsync();

            return Result<AddGameToSectionResponseDto>.Success(new AddGameToSectionResponseDto
            {
                SectionId = section.Id,
                GameId = game.Id,
                SectionTitleEn = section.TitleEn,
                GameTitleEn = game.TitleEn
            });
        }
    }
}
