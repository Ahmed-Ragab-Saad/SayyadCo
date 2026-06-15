using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Interfaces;
using System.Security.Claims;


namespace SayyadCo.Application.Features.Profiles.Queries.GetMyFunnyGames
{
    public class GetMyFunnyGamesQueryHandler : IRequestHandler<GetMyFunnyGamesQuery, Result<PagedResult<MyFunnyGameResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetMyFunnyGamesQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Result<PagedResult<MyFunnyGameResponseDto>>> Handle(GetMyFunnyGamesQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Result<PagedResult<MyFunnyGameResponseDto>>.Unauthorized("Unauthorized");

            var games = await _unitOfWork.UserGameRoles.GetFunnyGamesAsync(userId, request);
            var items = _mapper.Map<IEnumerable<MyFunnyGameResponseDto>>(games.Items);

            return Result<PagedResult<MyFunnyGameResponseDto>>.Success(
                new PagedResult<MyFunnyGameResponseDto>(items, games.TotalCount, games.PageNumber, games.PageSize));
        }
    }
}
