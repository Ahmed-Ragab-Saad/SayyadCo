using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Sections.Commands.CreateSection
{
    public class CreateSectionCommand : IRequest<Result<CreateSectionResponseDto>>
    {
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
