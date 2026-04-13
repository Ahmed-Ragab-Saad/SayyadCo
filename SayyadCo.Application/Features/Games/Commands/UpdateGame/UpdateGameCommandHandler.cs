using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Games.Commands.UpdateGame
{
    public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, Result<UpdateGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateGameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<UpdateGameResponseDto>> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _unitOfWork.Games.GetByIdAsync(request.Id);
            if (game is null)
                return Result<UpdateGameResponseDto>.NotFound("Game not found");

            _mapper.Map(request, game);
            _unitOfWork.Games.Update(game);
            await _unitOfWork.SaveChangesAsync();

            return Result<UpdateGameResponseDto>.Success(_mapper.Map<UpdateGameResponseDto>(game));
        }
    }
}
