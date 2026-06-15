using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SayyadCo.Application.Common.Results;
using SayyadCo.Application.Interfaces;
using System.Security.Claims;

namespace SayyadCo.Application.Features.Profiles.Queries.GetProfileInfo
{
    public class GetProfileInfoQueryHandler : IRequestHandler<GetProfileInfoQuery, Result<GetProfileInfoResponseDto>>
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetProfileInfoQueryHandler(IAuthService authService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Result<GetProfileInfoResponseDto>> Handle(GetProfileInfoQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Result<GetProfileInfoResponseDto>.Unauthorized("Unauthorized");

            var user = await _authService.FindByIdAsync(userId);
            if (user == null)
                return Result<GetProfileInfoResponseDto>.NotFound("User not found");

            return Result<GetProfileInfoResponseDto>.Success(_mapper.Map<GetProfileInfoResponseDto>(user));
        }
    }
}
