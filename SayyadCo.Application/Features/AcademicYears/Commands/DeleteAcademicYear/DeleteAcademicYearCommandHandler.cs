using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.AcademicYears.Commands.DeleteAcademicYear
{
    public class DeleteAcademicYearCommandHandler : IRequestHandler<DeleteAcademicYearCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAcademicYearCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteAcademicYearCommand request, CancellationToken cancellationToken)
        {
            var academicYear = await _unitOfWork.AcademicYears.GetByIdAsync(request.Id);
            if (academicYear is null)
                return Result<bool>.NotFound("Academic year not found");

            _unitOfWork.AcademicYears.Remove(academicYear);
            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
