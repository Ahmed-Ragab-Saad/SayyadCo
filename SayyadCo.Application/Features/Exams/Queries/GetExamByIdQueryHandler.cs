using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;
using System.Security.Claims;

namespace SayyadCo.Application.Features.Exams.Queries
{
    public class GetExamByIdQueryHandler : IRequestHandler<GetExamByIdQuery, Result<GetExamByIdResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGameAccessService _gameAccessService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetExamByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IGameAccessService gameAccessService,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _gameAccessService = gameAccessService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<GetExamByIdResponseDto>> Handle(GetExamByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Result<GetExamByIdResponseDto>.Unauthorized("Unauthorized");

            var access = await _gameAccessService.CheckAccessAsync(userId, request.SectionId, request.GameId);
            if (!access.HasAccess)
                return Result<GetExamByIdResponseDto>.Forbidden("You are not subscribed to this game");

            var exam = await _unitOfWork.Exams.GetByIdAsync(request.ExamId);
            if (exam is null)
                return Result<GetExamByIdResponseDto>.NotFound("Exam not found");

            if (exam.SectionId != request.SectionId ||
                exam.GameId != request.GameId ||
                exam.AcademicYearId != request.AcademicYearId ||
                exam.Semester != request.Semester ||
                exam.GroupId != request.GroupId)
                return Result<GetExamByIdResponseDto>.NotFound("Exam not found in this group, academic year, or semester");

            return Result<GetExamByIdResponseDto>.Success(_mapper.Map<GetExamByIdResponseDto>(exam));
        }
    }
}
