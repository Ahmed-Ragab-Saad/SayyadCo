using AutoMapper;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.Auth.Commands.Login;
using SayyadCo.Application.Features.Auth.Commands.RefreshToken;
using SayyadCo.Application.Features.Auth.Commands.Register;
using SayyadCo.Application.Features.Auth.Commands.VerifyEmail;

namespace SayyadCo.Application.Mappings
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterCommand, UserTokenModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Roles, opt => opt.Ignore())
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName.Trim()))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName.Trim()));

            CreateMap<TokenResult, VerifyEmailResponseDto>();

            CreateMap<TokenResult, LoginResponseDto>();

            CreateMap<TokenResult, RefreshTokenResponseDto>();
        }
    }
}
