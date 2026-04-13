using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Sections.Queries.GetSectionById
{
    public class GetSectionByIdQuery : IRequest<Result<GetSectionByIdResponseDto>>
    {
        public string Id { get; set; } = string.Empty;
    }
}
