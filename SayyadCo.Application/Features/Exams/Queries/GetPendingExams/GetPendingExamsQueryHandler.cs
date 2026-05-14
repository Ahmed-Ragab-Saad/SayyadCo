// GetPendingExamsQueryHandler.cs
using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Exams.Queries.GetPendingExams
{
    public class GetPendingExamsQueryHandler : IRequestHandler<GetPendingExamsQuery, Result<PagedResult<GetPendingExamsResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPendingExamsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PagedResult<GetPendingExamsResponseDto>>> Handle(
            GetPendingExamsQuery request, CancellationToken cancellationToken)
        {
            var exams = await _unitOfWork.Exams.GetPendingExamsAsync(request);

            var mappedItems = _mapper.Map<IEnumerable<GetPendingExamsResponseDto>>(exams.Items);

            var result = new PagedResult<GetPendingExamsResponseDto>(
                mappedItems,
                exams.TotalCount,
                exams.PageNumber,
                exams.PageSize);

            return Result<PagedResult<GetPendingExamsResponseDto>>.Success(result);
        }
    }
}