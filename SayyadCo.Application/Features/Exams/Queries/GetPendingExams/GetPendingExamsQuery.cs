using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;

namespace SayyadCo.Application.Features.Exams.Queries.GetPendingExams
{
    public class GetPendingExamsQuery : QueryParameters, IRequest<Result<PagedResult<GetPendingExamsResponseDto>>>
    {
    }
}
