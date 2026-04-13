using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.SectionGames.Commands.AddGameToSection
{
    public class AddGameToSectionCommand : IRequest<Result<AddGameToSectionResponseDto>>
    {
        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;
    }
}
