using MediatR;
using SayyadCo.Application.Common.DTOs;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;

namespace SayyadCo.Application.Features.Profiles.Queries.GetMyStudentGames
{
    public class GetMyStudentGamesQuery : QueryParameters, IRequest<Result<PagedResult<MyGameResponseDto>>>
    {
    }
}
