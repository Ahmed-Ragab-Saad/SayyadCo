using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Sections.Commands.UpdateSection
{
    public class UpdateSectionCommandHandler : IRequestHandler<UpdateSectionCommand, Result<UpdateSectionResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSectionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<UpdateSectionResponseDto>> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
        {
            var section = await _unitOfWork.Sections.GetByIdAsync(request.Id);
            if (section is null)
                return Result<UpdateSectionResponseDto>.NotFound("Section not found");

            _mapper.Map(request, section);
            _unitOfWork.Sections.Update(section);
            await _unitOfWork.SaveChangesAsync();

            return Result<UpdateSectionResponseDto>.Success(_mapper.Map<UpdateSectionResponseDto>(section));
        }
    }
}
