using MediatR;
using SayyadCo.Application.Common.Results;
using System.Text.Json.Serialization;

namespace SayyadCo.Application.Features.Tests.Commands.DeleteTest
{
    public class DeleteTestCommand : IRequest<Result<bool>>
    {
        [JsonIgnore]
        public string TestId { get; set; } = string.Empty;
        [JsonIgnore]
        public string SectionId { get; set; } = string.Empty;
        [JsonIgnore]
        public string GameId { get; set; } = string.Empty;
    }
}
