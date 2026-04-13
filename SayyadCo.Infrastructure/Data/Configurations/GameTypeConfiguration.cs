using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Infrastructure.Data.Configurations
{
    public class GameTypeConfiguration : IEntityTypeConfiguration<GameType>
    {
        public void Configure(EntityTypeBuilder<GameType> builder)
        {
            builder.ToTable("GameTypes");

            builder.HasKey(gt => gt.Id);

            builder.Property(gt => gt.NameAr)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(gt => gt.NameEn)
                .IsRequired()
                .HasMaxLength(100);

        }
    }
}
