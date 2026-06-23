using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Plans.Queries.GetPlanById
{
    public class GetPlanByIdQueryHandler : IRequestHandler<GetPlanByIdQuery, Result<GetPlanByIdResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPlanByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetPlanByIdResponseDto>> Handle(GetPlanByIdQuery request, CancellationToken cancellationToken)
        {
            var plan = await _unitOfWork.Plans.GetByIdAsync(request.Id);
            if (plan is null)
                return Result<GetPlanByIdResponseDto>.NotFound("Plan not found.");

            return Result<GetPlanByIdResponseDto>.Success(_mapper.Map<GetPlanByIdResponseDto>(plan));
        }
    }
}
