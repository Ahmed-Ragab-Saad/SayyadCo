using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayyadCo.Domain.Common;

namespace SayyadCo.Infrastructure.Data.Configurations
{
    public abstract class BaseUserGameConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseUserGame
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.SectionId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.GameId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.StartDate)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.ExpirationDate)
                .IsRequired();

            // Relationship
            builder.HasOne(x => x.SectionGame)
                .WithMany()
                .HasForeignKey(x => new { x.SectionId, x.GameId })
                .HasPrincipalKey(sg => new { sg.SectionId, sg.GameId })
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => new { x.SectionId, x.GameId });
        }
    }
}
