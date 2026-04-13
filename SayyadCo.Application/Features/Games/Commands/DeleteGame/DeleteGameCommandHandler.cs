using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Games.Commands.DeleteGame
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGameCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _unitOfWork.Games.GetByIdAsync(request.Id);
            if (game is null)
                return Result<bool>.NotFound("Game not found");

            _unitOfWork.Games.Remove(game);
            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
