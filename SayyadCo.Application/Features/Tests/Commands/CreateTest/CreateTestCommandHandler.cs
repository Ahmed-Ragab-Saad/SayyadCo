using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SayyadCo.Application.Common.Results;
using SayyadCo.Application.Interfaces;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces;
using System.Security.Claims;

namespace SayyadCo.Application.Features.Tests.Commands.CreateTest
{
    public class CreateTestCommandHandler : IRequestHandler<CreateTestCommand, Result<CreateTestResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthService _authService;

        public CreateTestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor,
            IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
        }

        public async Task<Result<CreateTestResponseDto>> Handle(CreateTestCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Result<CreateTestResponseDto>.Failure("Unauthorized");

            var sectionGame = await _unitOfWork.SectionGames.GetAsync(request.SectionId, request.GameId);
            if (sectionGame is null)
                return Result<CreateTestResponseDto>.NotFound("SectionGame not found");

            //var userHasTeacherRole = await _unitOfWork.UserGameRoles.UserHasRoleAsync(userId, "Teacher", request.SectionId, request.GameId);
            //if (!userHasTeacherRole)
            //    return Result<CreateTestResponseDto>.Failure("You are not subscribed to this game");

            var test = _mapper.Map<Test>(request);
            test.CreatedByUserId = userId;

            await _unitOfWork.Tests.AddAsync(test);


            var questionList = request.Questions
                .Select(q => new Question
                {
                    Image = q.Image,
                    ContentJson = q.ContentJson,
                    TestId = test.Id
                }).ToList();

            await _unitOfWork.Questions.AddQuestions(questionList);

            await _unitOfWork.SaveChangesAsync();

            return Result<CreateTestResponseDto>.Success(_mapper.Map<CreateTestResponseDto>(test));
        }
    }
}
