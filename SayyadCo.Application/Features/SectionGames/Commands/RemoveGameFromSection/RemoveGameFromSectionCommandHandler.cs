using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.SectionGames.Commands.RemoveGameFromSection
{
    public class RemoveGameFromSectionCommandHandler : IRequestHandler<RemoveGameFromSectionCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveGameFromSectionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(RemoveGameFromSectionCommand request, CancellationToken cancellationToken)
        {
            var sectionGame = await _unitOfWork.SectionGames.GetAsync(request.SectionId, request.GameId);
            if (sectionGame is null)
                return Result<bool>.NotFound("Game not found in this section");

            _unitOfWork.SectionGames.Remove(sectionGame);
            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
