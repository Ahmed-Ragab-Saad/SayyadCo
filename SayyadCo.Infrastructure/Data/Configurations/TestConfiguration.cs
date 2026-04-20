using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Infrastructure.Data.Configurations
{
    public class TestConfiguration : BaseAssessmentConfiguration<Test>
    {
        public override void Configure(EntityTypeBuilder<Test> builder)
        {
            base.Configure(builder);

            builder.ToTable("Tests");

            builder.HasOne(x => x.AcademicYear)
                .WithMany(a => a.Tests)
                .HasForeignKey(x => x.AcademicYearId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.Questions)
                .WithOne(q => q.Test)
                .HasForeignKey(q => q.TestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.SectionGame)
                .WithMany(sg => sg.Tests)
                .HasForeignKey(x => new { x.SectionId, x.GameId })
                .HasPrincipalKey(sg => new { sg.SectionId, sg.GameId })
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
