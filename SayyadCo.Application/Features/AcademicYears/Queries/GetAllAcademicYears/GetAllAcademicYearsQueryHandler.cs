using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.AcademicYears.Queries.GetAllAcademicYears
{
    public class GetAllAcademicYearsQueryHandler : IRequestHandler<GetAllAcademicYearsQuery, Result<PagedResult<GetAllAcademicYearsResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllAcademicYearsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PagedResult<GetAllAcademicYearsResponseDto>>> Handle(GetAllAcademicYearsQuery request, CancellationToken cancellationToken)
        {
            var academicYears = await _unitOfWork.AcademicYears.GetAllAsync(request);

            var mappedItems = _mapper.Map<IEnumerable<GetAllAcademicYearsResponseDto>>(academicYears.Items);

            var result = new PagedResult<GetAllAcademicYearsResponseDto>(
                mappedItems,
                academicYears.TotalCount,
                academicYears.PageNumber,
                academicYears.PageSize
            );

            return Result<PagedResult<GetAllAcademicYearsResponseDto>>.Success(result);
        }
    }
}
