using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Enums;
using System.Text.Json.Serialization;

namespace SayyadCo.Application.Features.Groups.Commands.DeleteGroup
{
    public class DeleteGroupCommand : IRequest<Result<bool>>
    {
        [JsonIgnore]
        public string SectionId { get; set; } = string.Empty;
        [JsonIgnore]
        public string GameId { get; set; } = string.Empty;
        [JsonIgnore]
        public string AcademicYearId { get; set; } = string.Empty;
        [JsonIgnore]
        public Semester Semester { get; set; }
        [JsonIgnore]
        public string GroupId { get; set; } = string.Empty;
    }
}
