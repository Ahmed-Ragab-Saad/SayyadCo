using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayyadCo.Domain.Common;

namespace SayyadCo.Infrastructure.Data.Configurations
{
    public abstract class BaseAssessmentConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseAssessment
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TitleAr)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.TitleEn)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.DescriptionAr)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.DescriptionEn)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.AcademicYearId)
                .IsRequired(false);

            builder.Property(x => x.Semester)
                .IsRequired(false);

            builder.Property(x => x.CreatedByUserId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.SectionId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.GameId)
                .IsRequired()
                .HasMaxLength(450);

            // Indexes
            builder.HasIndex(x => x.AcademicYearId);
            builder.HasIndex(x => x.CreatedByUserId);
            builder.HasIndex(x => x.CreatedAt);
        }
    }
}
