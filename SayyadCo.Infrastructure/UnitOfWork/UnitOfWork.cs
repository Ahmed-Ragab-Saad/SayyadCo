using SayyadCo.Domain.Interfaces;
using SayyadCo.Domain.Interfaces.Repositories;
using SayyadCo.Infrastructure.Data;

namespace SayyadCo.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IRefreshTokenRepository RefreshTokens { get; }
        public ISectoinRepository Sections { get; }
        public IOtpRepository Otps { get; }
        public IGameRepository Games { get; }
        public ISectionGameRepository SectionGames { get; }
        public IUserGameRoleRepository UserGameRoles { get; }
        public ITestRepository Tests { get; }
        public IQuestionRepository Questions { get; }
        public IAcademicYearRepository AcademicYears { get; }
        public ISectionGameAcademicYearRepository SectionGameAcademicYears { get; }
        public IGroupRepository Groups { get; }
        public IExamRepository Exams { get; }
        public IGroupMemberRepository GroupMembers { get; }

        public UnitOfWork(AppDbContext context, IRefreshTokenRepository refreshTokenRepository, IOtpRepository otps,
             ISectoinRepository sections, IGameRepository games, ISectionGameRepository sectionGames,
             IUserGameRoleRepository userGames, ITestRepository tests, IQuestionRepository questions,
             IAcademicYearRepository academicYears, ISectionGameAcademicYearRepository sectionGameAcademicYears,
             IGroupRepository groups, IExamRepository exams, IGroupMemberRepository groupMembers)
        {
            _context = context;
            RefreshTokens = refreshTokenRepository;
            Otps = otps;
            Sections = sections;
            Games = games;
            SectionGames = sectionGames;
            UserGameRoles = userGames;
            Tests = tests;
            Questions = questions;
            AcademicYears = academicYears;
            SectionGameAcademicYears = sectionGameAcademicYears;
            Groups = groups;
            Exams = exams;
            GroupMembers = groupMembers;
        }

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public void Dispose()
            => _context.Dispose();
    }
}
