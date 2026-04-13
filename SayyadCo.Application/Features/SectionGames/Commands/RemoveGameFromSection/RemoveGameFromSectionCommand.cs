using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.SectionGames.Commands.RemoveGameFromSection
{
    public class RemoveGameFromSectionCommand : IRequest<Result<bool>>
    {
        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;
    }
}
