using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Plans.Commands.UpdatePlan
{
    public class UpdatePlanCommandHandler : IRequestHandler<UpdatePlanCommand, Result<UpdatePlanResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePlanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<UpdatePlanResponseDto>> Handle(UpdatePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = await _unitOfWork.Plans.GetByIdAsync(request.Id);
            if (plan is null)
                return Result<UpdatePlanResponseDto>.NotFound("Plan not found");

            var englishTitleTask = await _unitOfWork.Plans.ExistingByTitle(request.TitleEn, request.Id);
            var arabicTitleTask = await _unitOfWork.Plans.ExistingByTitle(request.TitleAr, request.Id);

            var errors = new List<string>();

            if (englishTitleTask)
                errors.Add("English title already exists");

            if (arabicTitleTask)
                errors.Add("Arabic title already exists");

            if (errors.Count > 0)
                return Result<UpdatePlanResponseDto>.Failure(string.Join(Environment.NewLine, errors));

            _mapper.Map(request, plan);
            _unitOfWork.Plans.Update(plan);
            await _unitOfWork.SaveChangesAsync();

            return Result<UpdatePlanResponseDto>.Success(_mapper.Map<UpdatePlanResponseDto>(plan));
        }
    }
}
