using SayyadCo.Domain.Interfaces.Repositories;

namespace SayyadCo.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRefreshTokenRepository RefreshTokens { get; }
        IOtpRepository Otps { get; }
        ISectoinRepository Sections { get; }
        IGameRepository Games { get; }
        ISectionGameRepository SectionGames { get; }
        IUserGameRoleRepository UserGameRoles { get; }
        ITestRepository Tests { get; }
        IQuestionRepository Questions { get; }
        IAcademicYearRepository AcademicYears { get; }
        ISectionGameAcademicYearRepository SectionGameAcademicYears { get; }
        IGroupRepository Groups { get; }
        IExamRepository Exams { get; }
        IGroupMemberRepository GroupMembers { get; }
        IExamUpdateRequestRepository ExamUpdateRequests { get; }
        Task<int> SaveChangesAsync();
    }
}
