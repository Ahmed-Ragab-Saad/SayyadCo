using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Sections.Commands.DeleteSection
{
    internal class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSectionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
        {
            var section = await _unitOfWork.Sections.GetByIdAsync(request.Id);
            if (section is null)
                return Result<bool>.NotFound("Section not found");

            _unitOfWork.Sections.Remove(section);
            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
