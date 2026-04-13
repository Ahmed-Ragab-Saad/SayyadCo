using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;

namespace SayyadCo.Application.Features.Sections.Queries.GetAllSections
{
    public class GetAllSectionsQuery : QueryParameters, IRequest<Result<PagedResult<GetAllSectionsResponseDto>>>
    {
    }
}
