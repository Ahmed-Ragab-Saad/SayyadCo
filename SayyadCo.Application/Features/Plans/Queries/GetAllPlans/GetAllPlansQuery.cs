using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;

namespace SayyadCo.Application.Features.Plans.Queries.GetAllPlans
{
    public class GetAllPlansQuery : QueryParameters, IRequest<Result<PagedResult<GetAllPlansResponseDto>>>
    {
    }
}
