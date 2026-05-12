using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Enums;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Exams.Commands.ApproveExam
{
    public class ApproveExamCommandHandler : IRequestHandler<ApproveExamCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApproveExamCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(ApproveExamCommand request, CancellationToken cancellationToken)
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

            if (exam.Status == ExamStatus.Approved)
                return Result<bool>.Failure("Exam is already approved");

            exam.Status = ExamStatus.Approved;
            exam.RejectionReason = null;

            _unitOfWork.Exams.Update(exam);
            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
