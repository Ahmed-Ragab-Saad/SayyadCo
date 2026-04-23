using SayyadCo.Domain.Enums;

namespace SayyadCo.Application.Features.Groups.Queries.GetGroupById
{
    public class GroupExamDto
    {
        public string Id { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public ExamStatus Status { get; set; }
    }
}