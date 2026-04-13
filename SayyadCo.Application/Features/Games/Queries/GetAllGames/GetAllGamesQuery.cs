using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;

namespace SayyadCo.Application.Features.Games.Queries.GetAllGames
{
    public class GetAllGamesQuery : QueryParameters, IRequest<Result<PagedResult<GetAllGamesResponseDto>>>
    {
    }
}
