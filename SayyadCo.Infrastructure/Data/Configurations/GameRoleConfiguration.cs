using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Infrastructure.Data.Configurations
{
    public class GameRoleConfiguration : IEntityTypeConfiguration<GameRole>
    {
        public void Configure(EntityTypeBuilder<GameRole> builder)
        {
            builder.ToTable("GameRoles");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Role)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            // Indexes
            builder.HasIndex(x => x.Role)
                .IsUnique();
        }
    }
}
