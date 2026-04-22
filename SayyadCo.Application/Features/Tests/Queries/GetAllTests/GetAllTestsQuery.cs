using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Enums;

namespace SayyadCo.Application.Features.Tests.Queries.GetAllTests
{
    public class GetAllTestsQuery : QueryParameters, IRequest<Result<PagedResult<GetAllTestsResponseDto>>>
    {
        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;
        public string AcademicYearId { get; set; } = string.Empty;
        public Semester Semester { get; set; }
    }
}
