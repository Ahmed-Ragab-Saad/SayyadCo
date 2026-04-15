using MediatR;
using SayyadCo.Application.Common.DTOs;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;

namespace SayyadCo.Application.Features.SectionGames.Queries.GetFunnySectionGames
{
    public class GetFunnySectionGamesQuery : QueryParameters, IRequest<Result<PagedResult<GetSectionGamesResponseDto>>>
    {
    }
}
