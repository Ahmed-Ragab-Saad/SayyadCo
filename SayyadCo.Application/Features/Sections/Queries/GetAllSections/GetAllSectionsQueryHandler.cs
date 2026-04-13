using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Sections.Queries.GetAllSections
{
    internal class GetAllSectionsQueryHandler : IRequestHandler<GetAllSectionsQuery, Result<PagedResult<GetAllSectionsResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllSectionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PagedResult<GetAllSectionsResponseDto>>> Handle(GetAllSectionsQuery request, CancellationToken cancellationToken)
        {
            var sections = await _unitOfWork.Sections.GetAllAsync(request);

            var mappedItems = _mapper.Map<IEnumerable<GetAllSectionsResponseDto>>(sections.Items);

            var result = new PagedResult<GetAllSectionsResponseDto>(
                mappedItems,
                sections.TotalPages,
                sections.PageNumber,
                sections.PageSize
            );

            return Result<PagedResult<GetAllSectionsResponseDto>>.Success(result);
        }
    }
}
