using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;
using System.Security.Claims;

namespace SayyadCo.Application.Features.Tests.Queries.GetTestById
{
    public class GetTestByIdQueryHandler : IRequestHandler<GetTestByIdQuery, Result<GetTestByIdResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGameAccessService _gameAccessService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetTestByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IGameAccessService gameAccessService,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _gameAccessService = gameAccessService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<GetTestByIdResponseDto>> Handle(GetTestByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Result<GetTestByIdResponseDto>.Unauthorized("Unauthorized");

            var test = await _unitOfWork.Tests.GetByIdAsync(request.TestId);
            if (test is null)
                return Result<GetTestByIdResponseDto>.NotFound("Test not found");

            if (test.AcademicYearId != request.AcademicYearId)
                return Result<GetTestByIdResponseDto>.Failure("Invalid academic year");

            if (test.Semester != request.Semester)
                return Result<GetTestByIdResponseDto>.Failure("Invalid semester");

            var access = await _gameAccessService.CheckAccessAsync(userId, request.SectionId, request.GameId);
            if (!access.HasAccess)
                return Result<GetTestByIdResponseDto>.Forbidden("You are not subscribed to this game");

            return Result<GetTestByIdResponseDto>.Success(_mapper.Map<GetTestByIdResponseDto>(test));
        }
    }
}
