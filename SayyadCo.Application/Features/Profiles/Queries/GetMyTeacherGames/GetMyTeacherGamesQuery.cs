using MediatR;
using SayyadCo.Application.Common.DTOs;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;

namespace SayyadCo.Application.Features.Profiles.Queries.GetMyTeacherGames
{
    public class GetMyTeacherGamesQuery : QueryParameters, IRequest<Result<PagedResult<MyGameResponseDto>>>
    {
    }
}
