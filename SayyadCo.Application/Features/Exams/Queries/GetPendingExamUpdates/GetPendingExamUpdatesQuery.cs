using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;

namespace SayyadCo.Application.Features.Exams.Queries.GetPendingExamUpdates
{
    public class GetPendingExamUpdatesQuery : QueryParameters, IRequest<Result<PagedResult<GetPendingExamUpdatesResponseDto>>>
    {
    }
}
