using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Enums;

namespace SayyadCo.Infrastructure.Data.Configurations
{
    public class ExamConfiguration : BaseAssessmentConfiguration<Exam>
    {
        public override void Configure(EntityTypeBuilder<Exam> builder)
        {
            base.Configure(builder);

            builder.ToTable("Exams");

            builder.Property(x => x.Status)
                .IsRequired()
                .HasDefaultValue(ExamStatus.Pending);

            builder.Property(x => x.GroupId)
                .HasMaxLength(450);

            // Relationships
            builder.HasOne(x => x.Group)
                .WithMany(g => g.Exams)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationships
            //builder.HasOne(x => x.AcademicYear)
            //    .WithMany(a => a.Exams)
            //    .HasForeignKey(x => x.AcademicYearId)
            //    .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.Questions)
                .WithOne(q => q.Exam)
                .HasForeignKey(q => q.ExamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.SectionGame)
                .WithMany(sg => sg.Exams)
                .HasForeignKey(x => new { x.SectionId, x.GameId })
                .HasPrincipalKey(sg => new { sg.SectionId, sg.GameId })
                .OnDelete(DeleteBehavior.Restrict);

            // Index on Status for filtering
            builder.HasIndex(x => x.Status);
        }
    }
}
