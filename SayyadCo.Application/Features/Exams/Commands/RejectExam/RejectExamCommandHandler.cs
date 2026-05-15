using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Enums;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Exams.Commands.RejectExam
{
    public class RejectExamCommandHandler : IRequestHandler<RejectExamCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RejectExamCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(RejectExamCommand request, CancellationToken cancellationToken)
        {
            var exam = await _unitOfWork.Exams.GetByIdAsync(request.ExamId);
            if (exam is null)
                return Result<bool>.NotFound("Exam not found");

            if (exam.Status == ExamStatus.Rejected)
                return Result<bool>.Failure("Exam is already rejected");

            exam.Status = ExamStatus.Rejected;
            exam.RejectionReason = request.Reason;

            _unitOfWork.Exams.Update(exam);
            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
