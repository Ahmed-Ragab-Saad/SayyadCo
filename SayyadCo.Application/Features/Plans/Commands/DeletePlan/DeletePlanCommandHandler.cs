using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Plans.Commands.DeletePlan
{
    public class DeletePlanCommandHandler : IRequestHandler<DeletePlanCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePlanCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeletePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = await _unitOfWork.Plans.GetByIdAsync(request.Id);
            if (plan is null)
                return Result<bool>.NotFound("Plan not found");

            _unitOfWork.Plans.Remove(plan);
            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
