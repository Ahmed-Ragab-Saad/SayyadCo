using AutoMapper;
using SayyadCo.Application.Features.Games.Commands.CreateGame;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Application.Mappings
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<CreateGameCommand, Game>();
            CreateMap<Game, CreateGameResponseDto>();
        }
    }
}
