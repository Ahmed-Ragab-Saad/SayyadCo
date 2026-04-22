using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Tests.Commands.DeleteTest
{
    public class DeleteTestCommandHandler : IRequestHandler<DeleteTestCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _unitOfWork.Tests.GetByIdAsync(request.TestId);
            if (test is null)
                return Result<bool>.NotFound("Test not found");

            if (test.SectionId != request.SectionId || test.GameId != request.GameId)
                return Result<bool>.NotFound("Test not found in this SectionGame");

            if (test.AcademicYearId != request.AcademicYearId)
                return Result<bool>.Failure("Invalid academic year");

            if (test.Semester != request.Semester)
                return Result<bool>.Failure("Invalid semester");

            _unitOfWork.Tests.Remove(test);
            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
