using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.SectionGames.Commands.AssignPlans
{
    public class AssignPlansToSectionGameCommandHandler : IRequestHandler<AssignPlansToSectionGameCommand, Result<List<AssignPlansResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AssignPlansToSectionGameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<AssignPlansResponseDto>>> Handle(AssignPlansToSectionGameCommand request, CancellationToken cancellationToken)
        {
            var sectionGame = await _unitOfWork.SectionGames.GetAsync(request.SectionId, request.GameId);
            if (sectionGame is null)
                return Result<List<AssignPlansResponseDto>>.NotFound("SectionGame not found");

            var response = new List<AssignPlansResponseDto>();
            var errors = new List<string>();

            foreach (var planDto in request.Plans)
            {
                var plan = await _unitOfWork.Plans.GetByIdAsync(planDto.PlanId);
                if (plan is null)
                {
                    errors.Add($"Plan {planDto.PlanId} not found");
                    continue;
                }

                var existing = await _unitOfWork.SectionGamePlans
                    .GetAsync(planDto.PlanId, request.SectionId, request.GameId, planDto.PlanType);
                if (existing is not null)
                {
                    errors.Add($"Plan '{plan.TitleEn}' | '{plan.TitleAr}' already assigned as {planDto.PlanType}");
                    continue;
                }

                await _unitOfWork.SectionGamePlans.AddAsync(new SectionGamePlan
                {
                    PlanId = planDto.PlanId,
                    SectionId = request.SectionId,
                    GameId = request.GameId,
                    PlanType = planDto.PlanType
                });

                response.Add(new AssignPlansResponseDto
                {
                    PlanId = plan.Id,
                    PlanTitleEn = plan.TitleEn,
                    PlanTitleAr = plan.TitleAr,
                    Price = plan.Price,
                    DurationInDays = plan.DurationInDays,
                    PlanType = planDto.PlanType
                });
            }

            if (errors.Any() && !response.Any())
                return Result<List<AssignPlansResponseDto>>.Failure(string.Join(", ", errors));

            await _unitOfWork.SaveChangesAsync();

            return Result<List<AssignPlansResponseDto>>.Success(response);
        }
    }
}
