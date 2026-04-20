using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.AcademicYears.Commands.AddAcademicYear
{
    public class AddAcademicYearCommandHandler : IRequestHandler<AddAcademicYearCommand, Result<AddAcademicYearResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddAcademicYearCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AddAcademicYearResponseDto>> Handle(AddAcademicYearCommand request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.AcademicYears.ExistsByTitles(string.Empty, request.TitleAr, request.TitleEn))
                return Result<AddAcademicYearResponseDto>.Failure("An academic year with the same Arabic or English title already exists");

            var academicYear = _mapper.Map<AcademicYear>(request);

            await _unitOfWork.AcademicYears.AddAsync(academicYear);
            await _unitOfWork.SaveChangesAsync();

            return Result<AddAcademicYearResponseDto>.Success(_mapper.Map<AddAcademicYearResponseDto>(academicYear));
        }
    }
}
