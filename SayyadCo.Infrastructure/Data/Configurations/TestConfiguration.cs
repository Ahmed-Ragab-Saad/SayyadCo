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

            // Relationships
            builder.HasOne(x => x.Group)
                .WithMany(g => g.Tests)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.AcademicYear)
                .WithMany(a => a.Tests)
                .HasForeignKey(x => x.AcademicYearId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.Questions)
                .WithOne(q => q.Test)
                .HasForeignKey(q => q.TestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
