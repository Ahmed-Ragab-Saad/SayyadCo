using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SayyadCo.Domain.Entities;
using SayyadCo.Infrastructure.Identity;

namespace SayyadCo.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Section> Sections { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<SectionGame> SectionGames { get; set; }
        public DbSet<TeacherGame> TeacherGames { get; set; }
        public DbSet<StudentGame> StudentGames { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Code> Codes { get; set; }
        public DbSet<GameRole> GameRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<OtpCode> OtpCodes { get; set; }
        public DbSet<GameType> GameTypes { get; set; }
        public DbSet<UserGameRole> UserGameRoles { get; set; }
        public DbSet<SectionGameAcademicYear> SectionGameAcademicYears { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Alternative way: Apply all configurations automatically
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
