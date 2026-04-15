using MediatR;
using SayyadCo.Application.Common.DTOs;
using SayyadCo.Application.Common.Results;
using System.Text.Json.Serialization;

namespace SayyadCo.Application.Features.SectionGames.Commands.AddGameToSection
{
    public class AddGameToSectionCommand : IRequest<Result<AddGameToSectionResponseDto>>
    {
        [JsonIgnore]
        public string SectionId { get; set; } = string.Empty;
        public List<string> GamesIds { get; set; } = new();
    }
}
