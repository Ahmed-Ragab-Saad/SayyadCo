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

        public UnitOfWork(AppDbContext context, IRefreshTokenRepository refreshTokenRepository, IOtpRepository otps,
             ISectoinRepository sections, IGameRepository games, ISectionGameRepository sectionGames,
             IUserGameRoleRepository userGames, ITestRepository tests, IQuestionRepository questions)
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
        }

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public void Dispose()
            => _context.Dispose();
    }
}
