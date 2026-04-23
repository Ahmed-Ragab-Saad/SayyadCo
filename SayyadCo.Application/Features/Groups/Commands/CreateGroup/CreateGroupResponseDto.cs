using SayyadCo.Domain.Enums;

namespace SayyadCo.Application.Features.Groups.Commands.CreateGroup
{
    public class CreateGroupResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public bool IsPrivate { get; set; }
        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;
        public string AcademicYearId { get; set; } = string.Empty;
        public Semester Semester { get; set; }
        public string CreatedByUserId { get; set; } = string.Empty;
    }
}