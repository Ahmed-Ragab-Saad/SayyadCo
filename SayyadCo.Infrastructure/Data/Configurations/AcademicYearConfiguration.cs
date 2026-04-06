using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Infrastructure.Data.Configurations
{
    public class AcademicYearConfiguration : IEntityTypeConfiguration<AcademicYear>
    {
        public void Configure(EntityTypeBuilder<AcademicYear> builder)
        {
            builder.ToTable("AcademicYears");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TitleAr)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.TitleEn)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            // Indexes
            builder.HasIndex(x => x.TitleAr);
            builder.HasIndex(x => x.TitleAr);
        }
    }
}
