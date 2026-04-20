using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Tests.Commands.AcceptTest
{
    public class AcceptTestCommandHandler : IRequestHandler<AcceptTestCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AcceptTestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(AcceptTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _unitOfWork.Tests.GetByIdAsync(request.TestId);
            if (test is null)
                return Result<bool>.NotFound("Test not found");

            //test.Status = RequestStatus.Accepted;

            _unitOfWork.Tests.Update(test);
            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
