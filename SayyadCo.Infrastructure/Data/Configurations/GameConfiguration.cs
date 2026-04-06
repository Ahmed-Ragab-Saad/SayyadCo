using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Infrastructure.Data.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Games");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TitleAr)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.TitleEn)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            // Relationships
            builder.HasMany(x => x.SectionGames)
                .WithOne(sg => sg.Game)
                .HasForeignKey(sg => sg.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(x => x.TitleAr);
            builder.HasIndex(x => x.TitleEn);
        }
    }
}
