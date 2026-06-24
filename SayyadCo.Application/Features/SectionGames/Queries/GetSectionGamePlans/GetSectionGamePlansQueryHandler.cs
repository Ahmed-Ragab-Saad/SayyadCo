using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.SectionGames.Queries.GetSectionGamePlans
{
    public class GetSectionGamePlansQueryHandler : IRequestHandler<GetSectionGamePlansQuery, Result<List<GetSectionGamePlansResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSectionGamePlansQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetSectionGamePlansResponseDto>>> Handle(GetSectionGamePlansQuery request, CancellationToken cancellationToken)
        {
            var plans = await _unitOfWork.SectionGamePlans.GetBySectionGameAsync(request.SectionId, request.GameId);

            var response = _mapper.Map<IEnumerable<GetSectionGamePlansResponseDto>>(plans).ToList();

            return Result<List<GetSectionGamePlansResponseDto>>.Success(response);
        }
    }
}
