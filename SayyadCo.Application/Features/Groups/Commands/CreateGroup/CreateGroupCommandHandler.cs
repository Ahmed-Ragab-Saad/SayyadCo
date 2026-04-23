using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces;
using System.Security.Claims;

namespace SayyadCo.Application.Features.Groups.Commands.CreateGroup
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Result<CreateGroupResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGameAccessService _gameAccessService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IGameAccessService gameAccessService,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _gameAccessService = gameAccessService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<CreateGroupResponseDto>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Result<CreateGroupResponseDto>.Unauthorized("Unauthorized");

            var access = await _gameAccessService.CheckAccessAsync(userId, request.SectionId, request.GameId);
            if (!access.HasAccess)
                return Result<CreateGroupResponseDto>.Forbidden("You are not subscribed to this game");

            if (!access.IsTeacher && !access.IsAdminOrSuperAdmin)
                return Result<CreateGroupResponseDto>.Forbidden("Only Teachers and Admins can create groups");

            var sectionGame = await _unitOfWork.SectionGames.GetAsync(request.SectionId, request.GameId);
            if (sectionGame is null)
                return Result<CreateGroupResponseDto>.NotFound("SectionGame not found");

            var academicYear = await _unitOfWork.AcademicYears.GetByIdAsync(request.AcademicYearId);
            if (academicYear is null)
                return Result<CreateGroupResponseDto>.NotFound("Academic year not found");

            var sectionGameAcademicYear = await _unitOfWork.SectionGameAcademicYears
                .GetAsync(request.SectionId, request.GameId, request.AcademicYearId);

            if (sectionGameAcademicYear is null)
                return Result<CreateGroupResponseDto>.NotFound("Academic year not found in this SectionGame");

            if (access.IsTeacher)
            {
                var existingGroup = await _unitOfWork.Groups.GetTeacherGroupAsync(
                    userId, request.SectionId, request.GameId,
                    request.AcademicYearId, request.Semester);

                if (existingGroup is not null)
                    return Result<CreateGroupResponseDto>
                        .Failure("You already have a group in this SectionGame for this academic year and semester");
            }

            var group = new Group
            {
                Name = request.Name,
                Description = request.Description,
                Image = request.Image,
                IsPrivate = request.IsPrivate,
                Password = request.IsPrivate ? request.Password : null,
                SectionId = request.SectionId,
                GameId = request.GameId,
                AcademicYearId = request.AcademicYearId,
                Semester = request.Semester,
                CreatedByUserId = userId
            };

            await _unitOfWork.Groups.AddAsync(group);
            await _unitOfWork.SaveChangesAsync();

            return Result<CreateGroupResponseDto>.Success(_mapper.Map<CreateGroupResponseDto>(group));
        }
    }
}
