using SayyadCo.Domain.Common;

namespace SayyadCo.Domain.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty; //Default if null
        public bool IsPrivate { get; set; } = false;
        public string? Password { get; set; }

        //Navigation
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
        public ICollection<Test> Tests { get; set; } = new List<Test>();
    }
}
