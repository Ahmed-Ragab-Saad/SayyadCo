using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Enums;
using System.Text.Json.Serialization;

namespace SayyadCo.Application.Features.Tests.Queries.GetAllTests
{
    public class GetAllTestsQuery : QueryParameters, IRequest<Result<PagedResult<GetAllTestsResponseDto>>>
    {
        [JsonIgnore]
        public string SectionId { get; set; } = string.Empty;
        [JsonIgnore]
        public string GameId { get; set; } = string.Empty;
        [JsonIgnore]
        public string AcademicYearId { get; set; } = string.Empty;
        [JsonIgnore]
        public Semester Semester { get; set; }
    }
}
