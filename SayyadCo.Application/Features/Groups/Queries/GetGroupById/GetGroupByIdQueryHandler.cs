using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;
using System.Security.Claims;

namespace SayyadCo.Application.Features.Groups.Queries.GetGroupById
{
    public class GetGroupByIdQueryHandler : IRequestHandler<GetGroupByIdQuery, Result<GetGroupByIdResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGameAccessService _gameAccessService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetGroupByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IGameAccessService gameAccessService,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _gameAccessService = gameAccessService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<GetGroupByIdResponseDto>> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Result<GetGroupByIdResponseDto>.Unauthorized("Unauthorized");

            var access = await _gameAccessService.CheckAccessAsync(userId, request.SectionId, request.GameId);
            if (!access.HasAccess)
                return Result<GetGroupByIdResponseDto>.Forbidden("You are not subscribed to this game");

            var group = await _unitOfWork.Groups.GetByIdAsync(request.GroupId);
            if (group is null)
                return Result<GetGroupByIdResponseDto>.NotFound("Group not found");

            if (group.SectionId != request.SectionId || group.GameId != request.GameId ||
                group.AcademicYearId != request.AcademicYearId || group.Semester != request.Semester)
                return Result<GetGroupByIdResponseDto>.NotFound("Group not found in this SectionGame");

            if (group.IsPrivate && !access.IsAdminOrSuperAdmin && group.CreatedByUserId != userId)
            {
                var isMember = await _unitOfWork.GroupMembers.IsMemberAsync(userId, group.Id);
                if (!isMember)
                    return Result<GetGroupByIdResponseDto>.Forbidden("This group is private");
            }

            return Result<GetGroupByIdResponseDto>.Success(_mapper.Map<GetGroupByIdResponseDto>(group));
        }
    }
}
