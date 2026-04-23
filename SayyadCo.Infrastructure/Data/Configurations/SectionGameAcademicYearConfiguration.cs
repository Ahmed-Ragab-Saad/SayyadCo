using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Infrastructure.Data.Configurations
{
    public class SectionGameAcademicYearConfiguration : IEntityTypeConfiguration<SectionGameAcademicYear>
    {
        public void Configure(EntityTypeBuilder<SectionGameAcademicYear> builder)
        {
            builder.ToTable("SectionGameAcademicYears");

            builder.HasKey(x => new
            {
                x.SectionId,
                x.GameId,
                x.AcademicYearId
            });

            // Relations
            builder.HasOne(x => x.SectionGame)
                .WithMany(x => x.SectionGameAcademicYears)
                .HasForeignKey(x => new { x.SectionId, x.GameId })
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.AcademicYear)
                .WithMany(x => x.SectionGameAcademicYears)
                .HasForeignKey(x => x.AcademicYearId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
