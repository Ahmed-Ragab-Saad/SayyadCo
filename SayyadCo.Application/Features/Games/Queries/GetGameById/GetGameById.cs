using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Games.Queries.GetGameById
{
    public class GetGameByIdQuery : IRequest<Result<GetGameByIdResponseDto>>
    {
        public string Id { get; set; } = string.Empty;
    }
}
