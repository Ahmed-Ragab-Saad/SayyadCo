using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Interfaces;
using System.Security.Claims;

namespace SayyadCo.Application.Features.Groups.Queries
{
    public class GetAllGroupsQueryHandler : IRequestHandler<GetAllGroupsQuery, Result<PagedResult<GetAllGroupsResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGameAccessService _gameAccessService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAllGroupsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IGameAccessService gameAccessService,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _gameAccessService = gameAccessService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<PagedResult<GetAllGroupsResponseDto>>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Result<PagedResult<GetAllGroupsResponseDto>>.Unauthorized("Unauthorized");

            var access = await _gameAccessService.CheckAccessAsync(userId, request.SectionId, request.GameId);
            if (!access.HasAccess)
                return Result<PagedResult<GetAllGroupsResponseDto>>.Forbidden("You are not subscribed to this game");

            var groups = await _unitOfWork.Groups.GetAllAsync(request);

            var items = _mapper.Map<IEnumerable<GetAllGroupsResponseDto>>(groups.Items);

            var result = new PagedResult<GetAllGroupsResponseDto>(
                items,
                groups.TotalCount,
                groups.PageNumber,
                groups.PageSize);

            return Result<PagedResult<GetAllGroupsResponseDto>>.Success(result);
        }
    }
}
