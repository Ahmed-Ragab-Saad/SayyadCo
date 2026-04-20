namespace SayyadCo.Application.Interfaces
{
    public class GameAccessResult
    {
        public bool HasAccess { get; set; }
        public bool IsTeacher { get; set; }
        public bool IsStudent { get; set; }
        public bool IsAdminOrSuperAdmin { get; set; }

    }
}