using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Infrastructure.Data.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.Image)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.IsPrivate)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(x => x.Password)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.AcademicYearId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.Semester)
                .IsRequired()
                .HasConversion<int>();

            //Relashins
            builder.HasOne(x => x.SectionGame)
                .WithMany(sg => sg.Groups)
                .HasForeignKey(x => new { x.SectionId, x.GameId })
                .HasPrincipalKey(sg => new { sg.SectionId, sg.GameId })
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.SectionGameAcademicYear)
                .WithMany(sga => sga.Groups)
                .HasForeignKey(x => new { x.SectionId, x.GameId, x.AcademicYearId })
                .HasPrincipalKey(sga => new { sga.SectionId, sga.GameId, sga.AcademicYearId })
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(x => x.Name);
            builder.HasIndex(x => x.IsPrivate);
            builder.HasIndex(x => x.CreatedAt);
        }
    }
}
