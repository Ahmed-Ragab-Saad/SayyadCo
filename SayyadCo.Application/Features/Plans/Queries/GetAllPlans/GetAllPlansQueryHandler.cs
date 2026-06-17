using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Plans.Queries.GetAllPlans
{
    public class GetAllPlansQueryHandler : IRequestHandler<GetAllPlansQuery, Result<PagedResult<GetAllPlansResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPlansQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PagedResult<GetAllPlansResponseDto>>> Handle(GetAllPlansQuery request, CancellationToken cancellationToken)
        {
            var plans = await _unitOfWork.Plans.GetAllAsync(request);
            var mappedItems = _mapper.Map<IEnumerable<GetAllPlansResponseDto>>(plans.Items);

            return Result<PagedResult<GetAllPlansResponseDto>>.Success(
                new PagedResult<GetAllPlansResponseDto>(mappedItems, plans.TotalCount, plans.PageNumber, plans.PageSize));
        }
    }
}
