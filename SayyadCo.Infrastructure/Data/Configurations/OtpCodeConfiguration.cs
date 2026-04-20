using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayyadCo.Domain.Entities;
using SayyadCo.Infrastructure.Identity;

namespace SayyadCo.Infrastructure.Data.Configurations
{
    public class OtpCodeConfiguration : IEntityTypeConfiguration<OtpCode>
    {
        public void Configure(EntityTypeBuilder<OtpCode> builder)
        {
            builder.ToTable("OtpCodes");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.UserId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(o => o.Code)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(o => o.ExpiresAt)
                .IsRequired();

            builder.Property(o => o.IsUsed)
                .HasDefaultValue(false);

            builder.HasOne<ApplicationUser>()
                .WithMany(au => au.OtpCodes)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(o => o.UserId);
            builder.HasIndex(o => new { o.UserId, o.Code });

            builder.Ignore(o => o.IsExpired);
            builder.Ignore(o => o.IsValid);
        }
    }
}
