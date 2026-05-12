using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Interfaces;
using System.Security.Claims;

namespace SayyadCo.Application.Features.Tests.Queries.GetAllTests
{
    public class GetAllTestsQueryHandler : IRequestHandler<GetAllTestsQuery, Result<PagedResult<GetAllTestsResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGameAccessService _gameAccessService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAllTestsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IGameAccessService gameAccessService,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _gameAccessService = gameAccessService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<PagedResult<GetAllTestsResponseDto>>> Handle(GetAllTestsQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Result<PagedResult<GetAllTestsResponseDto>>.Unauthorized("Unauthorized");

            var access = await _gameAccessService.CheckAccessAsync(userId, request.SectionId, request.GameId);
            if (!access.HasAccess)
                return Result<PagedResult<GetAllTestsResponseDto>>.Forbidden("You are not subscribed to this game");

            var tests = await _unitOfWork.Tests.GetBySectionGameAsync(request.SectionId, request.GameId, request.AcademicYearId,
                request.Semester, request);

            var mappedItems = _mapper.Map<IEnumerable<GetAllTestsResponseDto>>(tests.Items);

            foreach (var item in mappedItems)
            {
                item.QuestionsCount = await _unitOfWork.Questions.GetQuestionsCount(item.Id);
            }

            var result = new PagedResult<GetAllTestsResponseDto>(
                mappedItems,
                tests.TotalCount,
                tests.PageNumber,
                tests.PageSize
            );

            return Result<PagedResult<GetAllTestsResponseDto>>.Success(result);
        }
    }
}
