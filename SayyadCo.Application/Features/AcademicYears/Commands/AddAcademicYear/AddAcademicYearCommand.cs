using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.AcademicYears.Commands.AddAcademicYear
{
    public class AddAcademicYearCommand : IRequest<Result<AddAcademicYearResponseDto>>
    {
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
    }
}
