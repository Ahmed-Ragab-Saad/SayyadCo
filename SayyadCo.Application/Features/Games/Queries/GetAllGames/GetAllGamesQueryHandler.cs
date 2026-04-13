using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Games.Queries.GetAllGames
{
    public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, Result<PagedResult<GetAllGamesResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllGamesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PagedResult<GetAllGamesResponseDto>>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
        {
            var games = await _unitOfWork.Games.GetAllAsync(request);

            var mappedItems = _mapper.Map<IEnumerable<GetAllGamesResponseDto>>(games.Items);

            var result = new PagedResult<GetAllGamesResponseDto>(
                mappedItems,
                games.TotalCount,
                games.PageNumber,
                games.PageSize
            );

            return Result<PagedResult<GetAllGamesResponseDto>>.Success(result);
        }
    }
}
