using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Infrastructure.Data.Configurations
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.ToTable("Plans", t =>
            {
                t.HasCheckConstraint("CK_Plan_Price", "Price >= 0");
                t.HasCheckConstraint("CK_Plan_Duration", "DurationInDays > 0");
            });

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TitleAr)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.TitleEn)
                .IsRequired()
                .HasMaxLength(200);

            //builder.Property(x => x.Name)
            //    .IsRequired()
            //    .HasMaxLength(100);

            builder.Property(x => x.DescriptionAr)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.DescriptionEn)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.DurationInDays)
                .IsRequired();

            builder.Property(x => x.Price)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            // Indexes
            //builder.HasIndex(x => x.Name)
            //    .IsUnique();

            builder.HasIndex(x => x.Price);
        }
    }
}
