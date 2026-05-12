using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Exams.Commands.DeleteExam
{
    public class DeleteExamCommandHandler : IRequestHandler<DeleteExamCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteExamCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        async Task<Result<bool>> IRequestHandler<DeleteExamCommand, Result<bool>>.Handle(DeleteExamCommand request, CancellationToken cancellationToken)
        {
            var exam = await _unitOfWork.Exams.GetByIdAsync(request.ExamId);
            if (exam is null)
                return Result<bool>.NotFound("Exam not found");

            if (exam.SectionId != request.SectionId ||
                exam.GameId != request.GameId ||
                exam.AcademicYearId != request.AcademicYearId ||
                exam.Semester != request.Semester ||
                exam.GroupId != request.GroupId)
                return Result<bool>.NotFound("Exam not found in this group, academic year, or semester");

            await _unitOfWork.Exams.DeleteWithQuestionsAsync(request.ExamId);
            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
