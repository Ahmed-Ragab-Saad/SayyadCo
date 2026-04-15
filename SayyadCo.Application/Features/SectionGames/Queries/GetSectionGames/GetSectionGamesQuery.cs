using MediatR;
using SayyadCo.Application.Common.DTOs;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;

namespace SayyadCo.Application.Features.SectionGames.Queries.GetSectionGames
{
    public class GetSectionGamesQuery : QueryParameters, IRequest<Result<PagedResult<GetSectionGamesResponseDto>>>
    {
        public string SectionId { get; set; } = string.Empty;
    }
}
