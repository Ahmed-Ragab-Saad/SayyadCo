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
        ITestRepository Tests { get; }
        IQuestionRepository Questions { get; }
        IAcademicYearRepository AcademicYears { get; }
        ISectionGameAcademicYearRepository SectionGameAcademicYears { get; }
        IGroupRepository Groups { get; }
        IExamRepository Exams { get; }
        IGroupMemberRepository GroupMembers { get; }
        IExamUpdateRequestRepository ExamUpdateRequests { get; }
        IPlanRepository Plans { get; }
        ISectionGamePlanRepository SectionGamePlans { get; }
        ICodeRepository Codes { get; }
        IGameRoleRepository GameRoles { get; }
        IUserGameRepository UserGames { get; }
        Task<int> SaveChangesAsync();
    }
}
