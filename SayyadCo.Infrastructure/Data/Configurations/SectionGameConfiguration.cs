using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Infrastructure.Data.Configurations
{
    public class SectionGameConfiguration : IEntityTypeConfiguration<SectionGame>
    {
        public void Configure(EntityTypeBuilder<SectionGame> builder)
        {
            builder.ToTable("SectionGames");

            // Composite Primary Key
            builder.HasKey(x => new { x.SectionId, x.GameId });

            builder.Property(x => x.SectionId)
                .IsRequired();

            builder.Property(x => x.GameId)
                .IsRequired();

            // Indexes
            builder.HasIndex(x => x.SectionId);
            builder.HasIndex(x => x.GameId);
        }
    }
}
