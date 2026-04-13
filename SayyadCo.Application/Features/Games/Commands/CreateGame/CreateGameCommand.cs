using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Games.Commands.CreateGame
{
    public class CreateGameCommand : IRequest<Result<CreateGameResponseDto>>
    {
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        //public string GameTypeId { get; set; } = string.Empty;
    }
}
