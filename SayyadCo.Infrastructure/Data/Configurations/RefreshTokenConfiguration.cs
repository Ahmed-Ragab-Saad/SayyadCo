using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayyadCo.Domain.Entities;
using SayyadCo.Infrastructure.Identity;

namespace SayyadCo.Infrastructure.Data.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.Token)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(rt => rt.UserId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(rt => rt.ExpiresAt)
                .IsRequired();

            builder.Property(rt => rt.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(rt => rt.IsRevoked)
                .HasDefaultValue(false);

            builder.Property(rt => rt.RevokedAt)
                .IsRequired(false);

            builder.HasOne<ApplicationUser>()
                .WithMany(au => au.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
