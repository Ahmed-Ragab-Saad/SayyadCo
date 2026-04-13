using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Sections.Queries.GetSectionById
{
    internal class GetSectionByIdQueryHandler : IRequestHandler<GetSectionByIdQuery, Result<GetSectionByIdResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSectionByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetSectionByIdResponseDto>> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
        {
            var section = await _unitOfWork.Sections.GetByIdAsync(request.Id);
            if (section is null)
                return Result<GetSectionByIdResponseDto>.NotFound("Section not found");

            return Result<GetSectionByIdResponseDto>.Success(_mapper.Map<GetSectionByIdResponseDto>(section));
        }
    }
}
