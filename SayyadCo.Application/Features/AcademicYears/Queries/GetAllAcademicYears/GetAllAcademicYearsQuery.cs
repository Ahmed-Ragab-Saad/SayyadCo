using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;

namespace SayyadCo.Application.Features.AcademicYears.Queries.GetAllAcademicYears
{
    public class GetAllAcademicYearsQuery : QueryParameters, IRequest<Result<PagedResult<GetAllAcademicYearsResponseDto>>>
    {
    }
}
