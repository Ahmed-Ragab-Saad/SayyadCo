using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Plans.Commands.CreatePlan
{
    public class CreatePlanCommandHandler : IRequestHandler<CreatePlanCommand, Result<CreatePlanResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePlanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CreatePlanResponseDto>> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
        {
            var englishTitleTask = await _unitOfWork.Plans.ExistingByTitle(request.TitleEn);
            var arabicTitleTask = await _unitOfWork.Plans.ExistingByTitle(request.TitleAr);

            var errors = new List<string>();

            if (englishTitleTask)
                errors.Add("English title already exists");

            if (arabicTitleTask)
                errors.Add("Arabic title already exists");

            if (errors.Count > 0)
                return Result<CreatePlanResponseDto>.Failure(string.Join(Environment.NewLine, errors));

            var plan = _mapper.Map<Plan>(request);
            await _unitOfWork.Plans.AddAsync(plan);
            await _unitOfWork.SaveChangesAsync();

            return Result<CreatePlanResponseDto>.Success(_mapper.Map<CreatePlanResponseDto>(plan));
        }
    }
}
