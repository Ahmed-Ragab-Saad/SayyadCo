using MediatR;
using SayyadCo.Application.Common.DTOs;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.SectionGames.Commands.AddGameToFunnySection
{
    public class AddGameToFunnySectionCommand : IRequest<Result<AddGameToSectionResponseDto>>
    {
        public List<string> GamesIds { get; set; } = new();
    }
}
