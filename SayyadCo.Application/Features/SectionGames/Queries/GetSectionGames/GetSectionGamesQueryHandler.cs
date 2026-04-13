using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.SectionGames.Queries.GetSectionGames
{
    public class GetSectionGamesQueryHandler : IRequestHandler<GetSectionGamesQuery, Result<PagedResult<GetSectionGamesResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSectionGamesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PagedResult<GetSectionGamesResponseDto>>> Handle(GetSectionGamesQuery request, CancellationToken cancellationToken)
        {
            var secction = await _unitOfWork.Sections.GetByIdAsync(request.SectionId);
            if (secction is null)
                return Result<PagedResult<GetSectionGamesResponseDto>>.NotFound("Section not found");

            var sectionGames = await _unitOfWork.SectionGames.GetBySectionIdAsync(request.SectionId, request);

            var mappedItems = sectionGames.Items.Select(sg => new GetSectionGamesResponseDto
            {
                GameId = sg.Game.Id,
                TitleAr = sg.Game.TitleAr,
                TitleEn = sg.Game.TitleEn,
                DescriptionAr = sg.Game.DescriptionAr,
                DescriptionEn = sg.Game.DescriptionEn,
                Image = sg.Game.Image
            });

            var result = new PagedResult<GetSectionGamesResponseDto>(
                mappedItems,
                sectionGames.TotalCount,
                sectionGames.PageNumber,
                sectionGames.PageSize
            );

            return Result<PagedResult<GetSectionGamesResponseDto>>.Success(result);
        }
    }
}
