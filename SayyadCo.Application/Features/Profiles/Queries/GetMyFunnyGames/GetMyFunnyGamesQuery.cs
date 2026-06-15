using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;

namespace SayyadCo.Application.Features.Profiles.Queries.GetMyFunnyGames
{
    public class GetMyFunnyGamesQuery : QueryParameters, IRequest<Result<PagedResult<MyFunnyGameResponseDto>>>
    {
    }
}
