using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.AcademicYears.Commands.UpdateAcademicYear
{
    public class UpdateAcademicYearCommandHandler : IRequestHandler<UpdateAcademicYearCommand, Result<UpdateAcademicYearResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAcademicYearCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<UpdateAcademicYearResponseDto>> Handle(UpdateAcademicYearCommand request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.AcademicYears.ExistsByTitles(request.Id, request.TitleAr, request.TitleEn))
                return Result<UpdateAcademicYearResponseDto>.Failure("An academic year with the same Arabic or English title already exists");

            var academicYear = await _unitOfWork.AcademicYears.GetByIdAsync(request.Id);
            if (academicYear is null)
                return Result<UpdateAcademicYearResponseDto>.NotFound("Academic year not found");

            _mapper.Map(request, academicYear);

            _unitOfWork.AcademicYears.Update(academicYear);
            await _unitOfWork.SaveChangesAsync();

            return Result<UpdateAcademicYearResponseDto>.Success(_mapper.Map<UpdateAcademicYearResponseDto>(academicYear));
        }
    }
}
