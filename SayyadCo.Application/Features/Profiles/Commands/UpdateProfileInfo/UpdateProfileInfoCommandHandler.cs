using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SayyadCo.Application.Common.Results;
using SayyadCo.Application.Interfaces;
using System.Security.Claims;

namespace SayyadCo.Application.Features.Profiles.Commands.UpdateProfileInfo
{
    public class UpdateProfileInfoCommandHandler : IRequestHandler<UpdateProfileInfoCommand, Result<UpdateProfileInfoResponseDto>>
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UpdateProfileInfoCommandHandler(IAuthService authService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Result<UpdateProfileInfoResponseDto>> Handle(UpdateProfileInfoCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Result<UpdateProfileInfoResponseDto>.Unauthorized("Unauthorized");

            var success = await _authService
                .UpdateProfileAsync(userId, request.FirstName.Trim(), request.LastName.Trim(), request.Image);

            if (!success)
                return Result<UpdateProfileInfoResponseDto>.Failure("Failed to update profile");

            return Result<UpdateProfileInfoResponseDto>.Success(_mapper.Map<UpdateProfileInfoResponseDto>(request));
        }
    }
}
