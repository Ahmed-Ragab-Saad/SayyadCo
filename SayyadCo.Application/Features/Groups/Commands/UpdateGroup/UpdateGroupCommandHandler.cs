using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;
using System.Security.Claims;

namespace SayyadCo.Application.Features.Groups.Commands.UpdateGroup
{
    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, Result<UpdateGroupResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGameAccessService _gameAccessService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IGameAccessService gameAccessService,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _gameAccessService = gameAccessService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<UpdateGroupResponseDto>> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Result<UpdateGroupResponseDto>.Unauthorized("Unauthorized");

            var access = await _gameAccessService.CheckAccessAsync(userId, request.SectionId, request.GameId);
            if (!access.HasAccess)
                return Result<UpdateGroupResponseDto>.Forbidden("You are not subscribed to this game");

            var group = await _unitOfWork.Groups.GetByIdAsync(request.GroupId);
            if (group is null)
                return Result<UpdateGroupResponseDto>.NotFound("Group not found");

            if (group.SectionId != request.SectionId || group.GameId != request.GameId ||
                group.AcademicYearId != request.AcademicYearId || group.Semester != request.Semester)
                return Result<UpdateGroupResponseDto>.NotFound("Group not found in this SectionGame");

            if (!access.IsAdminOrSuperAdmin && group.CreatedByUserId != userId)
                return Result<UpdateGroupResponseDto>.Forbidden("You can only update your own group");

            _mapper.Map(request, group);

            _unitOfWork.Groups.Update(group);
            await _unitOfWork.SaveChangesAsync();

            return Result<UpdateGroupResponseDto>.Success(_mapper.Map<UpdateGroupResponseDto>(group));
        }
    }
}
