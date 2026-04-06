using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Infrastructure.Data.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Image)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.Property(x => x.Option1)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Option2)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Option3)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Option4)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.CorrectAnswer)
                .IsRequired();

            builder.Property(x => x.ExamId)
                .IsRequired(false);

            builder.Property(x => x.TestId)
                .IsRequired(false);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            // Indexes
            builder.HasIndex(x => x.ExamId);
            builder.HasIndex(x => x.TestId);


            builder.ToTable("Questions", t =>
            {
                t.HasCheckConstraint(
                    "CK_Question_ExamOrTest",
                    "(ExamId IS NOT NULL AND TestId IS NULL) OR (ExamId IS NULL AND TestId IS NOT NULL)"
                );
            });
        }
    }
}
