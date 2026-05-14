using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Exams.Queries.GetPendingExamUpdates
{
    public class GetPendingExamUpdatesQueryHandler : IRequestHandler<GetPendingExamUpdatesQuery, Result<PagedResult<GetPendingExamUpdatesResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPendingExamUpdatesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PagedResult<GetPendingExamUpdatesResponseDto>>> Handle(GetPendingExamUpdatesQuery request, CancellationToken cancellationToken)
        {
            var updates = await _unitOfWork.ExamUpdateRequests.GetPendingUpdatesAsync(request);

            var mappedItems = _mapper.Map<IEnumerable<GetPendingExamUpdatesResponseDto>>(updates.Items);

            var result = new PagedResult<GetPendingExamUpdatesResponseDto>(
                mappedItems,
                updates.TotalCount,
                updates.PageNumber,
                updates.PageSize);

            return Result<PagedResult<GetPendingExamUpdatesResponseDto>>.Success(result);
        }
    }
}