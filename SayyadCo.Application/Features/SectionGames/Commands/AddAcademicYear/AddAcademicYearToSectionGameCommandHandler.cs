using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.SectionGames.Commands.AddAcademicYear
{
    public class AddAcademicYearToSectionGameCommandHandler : IRequestHandler<AddAcademicYearToSectionGameCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddAcademicYearToSectionGameCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(AddAcademicYearToSectionGameCommand request, CancellationToken cancellationToken)
        {
            bool sectionGameExist = await _unitOfWork.SectionGames.ExistingAsync(request.SectionId, request.GameId);
            if (!sectionGameExist)
                return Result<bool>.NotFound("Game not found");

            var academicYearExist = await _unitOfWork.AcademicYears.ExistingAsync(request.AcademicYearId);
            if (!academicYearExist)
                return Result<bool>.NotFound("Academic year not found");

            await _unitOfWork.SectionGameAcademicYears.AddAsync(new SectionGameAcademicYear()
            {
                AcademicYearId = request.AcademicYearId,
                SectionId = request.SectionId,
                GameId = request.GameId
            });

            return Result<bool>.Success(true);
        }
    }
}
