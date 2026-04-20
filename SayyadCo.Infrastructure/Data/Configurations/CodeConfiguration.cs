using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Infrastructure.Data.Configurations
{
    public class CodeConfiguration : IEntityTypeConfiguration<Code>
    {
        public void Configure(EntityTypeBuilder<Code> builder)
        {
            builder.ToTable("Codes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(50);

            // ✅ بدل SectionGameId
            builder.Property(x => x.SectionId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.GameId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.UserId)
                .HasMaxLength(450); // ✅ مش Required لأنه Nullable

            builder.Property(x => x.GameRoleId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.IsUsed)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            // ✅ Composite FK
            builder.HasOne(x => x.SectionGame)
                .WithMany(sg => sg.Codes)
                .HasForeignKey(x => new { x.SectionId, x.GameId })
                .HasPrincipalKey(sg => new { sg.SectionId, sg.GameId })
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.GameRole)
                .WithMany(gr => gr.Codes)
                .HasForeignKey(x => x.GameRoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(x => x.Value).IsUnique();
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => new { x.SectionId, x.GameId }); // ✅
            builder.HasIndex(x => x.GameRoleId);
            builder.HasIndex(x => x.IsUsed);
        }
    }
}