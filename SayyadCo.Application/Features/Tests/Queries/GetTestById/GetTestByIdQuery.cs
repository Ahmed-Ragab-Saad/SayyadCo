using MediatR;
using SayyadCo.Application.Common.Results;
using System.Text.Json.Serialization;

namespace SayyadCo.Application.Features.Tests.Queries.GetTestById
{
    public class GetTestByIdQuery : IRequest<Result<GetTestByIdResponseDto>>
    {
        [JsonIgnore]
        public string TestId { get; set; } = string.Empty;
        [JsonIgnore]
        public string SectionId { get; set; } = string.Empty;
        [JsonIgnore]
        public string GameId { get; set; } = string.Empty;
    }
}
