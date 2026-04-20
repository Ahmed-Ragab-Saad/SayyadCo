using MediatR;
using SayyadCo.Application.Common.Results;
using System.Text.Json.Serialization;

namespace SayyadCo.Application.Features.AcademicYears.Commands.UpdateAcademicYear
{
    public class UpdateAcademicYearCommand : IRequest<Result<UpdateAcademicYearResponseDto>>
    {
        [JsonIgnore]
        public string Id { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
    }
}
