using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SayyadCo.Application.Common.DTOs;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Interfaces;
using System.Security.Claims;

namespace SayyadCo.Application.Features.Profiles.Queries.GetMyTeacherGames
{
    public class GetMyTeacherGamesQueryHandler : IRequestHandler<GetMyTeacherGamesQuery, Result<PagedResult<MyGameResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetMyTeacherGamesQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PagedResult<MyGameResponseDto>>> Handle(GetMyTeacherGamesQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Result<PagedResult<MyGameResponseDto>>.Unauthorized("Unauthorized");

            var games = await _unitOfWork.UserGames.GetTeacherGamesAsync(userId, request);

            var mappedItems = _mapper.Map<IEnumerable<MyGameResponseDto>>(games.Items);

            return Result<PagedResult<MyGameResponseDto>>.Success(
                new PagedResult<MyGameResponseDto>(mappedItems, games.TotalCount, games.PageNumber, games.PageSize));
        }
    }
}
