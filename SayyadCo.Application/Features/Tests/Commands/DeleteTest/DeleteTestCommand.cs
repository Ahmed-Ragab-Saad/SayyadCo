using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Enums;

namespace SayyadCo.Application.Features.Tests.Commands.DeleteTest
{
    public class DeleteTestCommand : IRequest<Result<bool>>
    {
        public string TestId { get; set; } = string.Empty;
        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;
        public string AcademicYearId { get; set; } = string.Empty;
        public Semester Semester { get; set; }
    }
}
