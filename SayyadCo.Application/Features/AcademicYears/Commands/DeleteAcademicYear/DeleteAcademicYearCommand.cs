using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.AcademicYears.Commands.DeleteAcademicYear
{
    public class DeleteAcademicYearCommand : IRequest<Result<bool>>
    {
        //[JsonIgnore]
        public string Id { get; set; } = string.Empty;
    }
}
