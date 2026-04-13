using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Games.Commands.DeleteGame
{
    public class DeleteGameCommand : IRequest<Result<bool>>
    {
        public string Id { get; set; } = string.Empty;
    }
}
