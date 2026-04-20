using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SayyadCo.Application.Common.Results;
using SayyadCo.Application.Interfaces;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Enums;
using SayyadCo.Domain.Interfaces;
using System.Security.Claims;

namespace SayyadCo.Application.Features.Tests.Commands.UpdateTest
{
    public class UpdateTestCommandHandler : IRequestHandler<UpdateTestCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public UpdateTestCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper,
            IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<Result<bool>> Handle(UpdateTestCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Result<bool>.Failure("Unauthorized");

            var test = await _unitOfWork.Tests.GetByIdAsync(request.TestId);
            if (test is null)
                return Result<bool>.NotFound("Test not found");

            _mapper.Map(request, test);
            _unitOfWork.Tests.Update(test);

            var existingQuestionsDict = test.Questions.ToDictionary(q => q.Id);
            var requestQuestionsDict = request.Questions
                .Where(q => !string.IsNullOrEmpty(q.Id))
                .ToDictionary(q => q.Id!);

            //Delete
            var toDelete = test.Questions
                .Where(q => !requestQuestionsDict.ContainsKey(q.Id))
                .ToList();

            foreach (var question in toDelete)
                _unitOfWork.Questions.Remove(question);

            //Update + Add
            RequestStatus status;
            if (await _authService.IsAdminOrSuperAdmin(userId))
            {
                status = RequestStatus.Accepted;
            }
            else
            {
                status = RequestStatus.Pending;
            }
            foreach (var questionDto in request.Questions)
            {
                if (!string.IsNullOrEmpty(questionDto.Id) && existingQuestionsDict.TryGetValue(questionDto.Id, out var existing))
                {
                    //Update
                    _mapper.Map(questionDto, existing);
                    existing.Status = status;
                    _unitOfWork.Questions.Update(existing);
                }
                else
                {
                    //Add
                    var q = _mapper.Map<Question>(questionDto);
                    q.TestId = test.Id;
                    q.Status = status;
                    await _unitOfWork.Questions.AddAsync(q);
                }
            }

            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
