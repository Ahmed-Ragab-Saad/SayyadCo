using SayyadCo.Domain.Common;

namespace SayyadCo.Domain.Entities
{
    public class SectionGame : Entity
    {
        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;

        //Navigations
        public Section Section { get; set; } = null!;
        public Game Game { get; set; } = null!;
        public ICollection<SectionGameAcademicYear> SectionGameAcademicYears { get; set; } = new List<SectionGameAcademicYear>();
        public ICollection<TeacherGame> TeacherGames { get; set; } = new List<TeacherGame>();
        public ICollection<StudentGame> StudentGames { get; set; } = new List<StudentGame>();
        public ICollection<Code> Codes { get; set; } = new List<Code>();
        public ICollection<Group> Groups { get; set; } = new List<Group>();
        public ICollection<Test> Tests { get; set; } = new List<Test>();
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
    }
}
