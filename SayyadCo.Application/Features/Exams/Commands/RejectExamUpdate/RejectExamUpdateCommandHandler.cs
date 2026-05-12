using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Enums;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Exams.Commands.RejectExamUpdate
{
    public class RejectExamUpdateCommandHandler : IRequestHandler<RejectExamUpdateCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RejectExamUpdateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(RejectExamUpdateCommand request, CancellationToken cancellationToken)
        {
            var updateRequest = await _unitOfWork.ExamUpdateRequests.GetByIdAsync(request.RequestId);
            if (updateRequest is null)
                return Result<bool>.NotFound("Update request not found");

            if (updateRequest.Status != RequestStatus.Pending)
                return Result<bool>.Failure("This request has already been processed");

            updateRequest.Status = RequestStatus.Rejected;
            updateRequest.RejectionReason = request.Reason;

            _unitOfWork.ExamUpdateRequests.Update(updateRequest);
            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
