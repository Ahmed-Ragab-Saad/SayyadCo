using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Infrastructure.Data.Configurations
{
    public class UserGameConfiguration : IEntityTypeConfiguration<UserGame>
    {
        public virtual void Configure(EntityTypeBuilder<UserGame> builder)
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

            builder.Property(x => x.GameRoleId)
                .IsRequired();

            // Relationship
            builder.HasOne(x => x.SectionGame)
                .WithMany(sg => sg.UserGames)
                .HasForeignKey(x => new { x.SectionId, x.GameId })
                .HasPrincipalKey(sg => new { sg.SectionId, sg.GameId })
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.GameRole)
                .WithMany(gr => gr.UserGames)
                .HasForeignKey(x => x.GameRoleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => new { x.SectionId, x.GameId });
        }
    }
}
