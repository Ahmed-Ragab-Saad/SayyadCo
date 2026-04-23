using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Groups.Commands.DeleteGroup
{
    public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGroupCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _unitOfWork.Groups.GetByIdAsync(request.GroupId);
            if (group is null)
                return Result<bool>.NotFound("Group not found");

            if (group.SectionId != request.SectionId || group.GameId != request.GameId ||
                group.AcademicYearId != request.AcademicYearId || group.Semester != request.Semester)
                return Result<bool>.NotFound("Group not found in this SectionGame");

            _unitOfWork.Groups.Remove(group);
            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
