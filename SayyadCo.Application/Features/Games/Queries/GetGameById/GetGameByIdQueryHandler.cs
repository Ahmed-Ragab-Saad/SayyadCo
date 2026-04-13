using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Games.Queries.GetGameById
{
    public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, Result<GetGameByIdResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetGameByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetGameByIdResponseDto>> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
        {
            var game = await _unitOfWork.Games.GetByIdAsync(request.Id);
            if (game is null)
                return Result<GetGameByIdResponseDto>.NotFound("Game not found");

            return Result<GetGameByIdResponseDto>.Success(_mapper.Map<GetGameByIdResponseDto>(game));
        }
    }
}
