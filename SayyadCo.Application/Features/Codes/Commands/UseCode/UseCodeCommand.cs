using MediatR;
using SayyadCo.Application.Common.Results;
using System.Text.Json.Serialization;

namespace SayyadCo.Application.Features.Codes.Commands.UseCode
{
    public class UseCodeCommand : IRequest<Result<UseCodeResponseDto>>
    {
        [JsonIgnore]
        public string SectionId { get; set; } = string.Empty;
        [JsonIgnore]
        public string GameId { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
