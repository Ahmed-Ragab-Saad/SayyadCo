using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Sections.Commands.CreateSection
{
    public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, Result<CreateSectionResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSectionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CreateSectionResponseDto>> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            var section = _mapper.Map<Section>(request);
            await _unitOfWork.Sections.AddAsync(section);
            await _unitOfWork.SaveChangesAsync();
            return Result<CreateSectionResponseDto>.Success(_mapper.Map<CreateSectionResponseDto>(section));
        }
    }
}
