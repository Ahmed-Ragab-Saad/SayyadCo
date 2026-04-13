using AutoMapper;
using SayyadCo.Application.Features.Games.Commands.CreateGame;
using SayyadCo.Application.Features.Games.Commands.UpdateGame;
using SayyadCo.Application.Features.Games.Queries.GetAllGames;
using SayyadCo.Application.Features.Games.Queries.GetGameById;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Application.Mappings
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<CreateGameCommand, Game>();
            CreateMap<Game, CreateGameResponseDto>();

            CreateMap<UpdateGameCommand, Game>();
            CreateMap<Game, UpdateGameResponseDto>();

            CreateMap<Game, GetAllGamesResponseDto>();

            CreateMap<Game, GetGameByIdResponseDto>();
        }
    }
}
