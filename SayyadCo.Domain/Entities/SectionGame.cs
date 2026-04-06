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
        public ICollection<TeacherGame> TeacherGames { get; set; } = new List<TeacherGame>();
        public ICollection<StudentGame> StudentGames { get; set; } = new List<StudentGame>();
        public ICollection<Code> Codes { get; set; } = new List<Code>();
    }
}
