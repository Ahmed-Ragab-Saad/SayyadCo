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

            builder.Property(g => g.DescriptionAr)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(g => g.DescriptionEn)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.CreatedAt)
                .IsRequired();


            // Relationships
            builder.HasMany(x => x.SectionGames)
                .WithOne(sg => sg.Game)
                .HasForeignKey(sg => sg.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(g => g.GameType)
                .WithMany(gt => gt.Games)
                .HasForeignKey(g => g.GameTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(x => x.TitleAr);
            builder.HasIndex(x => x.TitleEn);
        }
    }
}
