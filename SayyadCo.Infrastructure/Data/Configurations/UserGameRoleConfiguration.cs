using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayyadCo.Infrastructure.Identity;

namespace SayyadCo.Infrastructure.Data.Configurations
{
    public class UserGameRoleConfiguration : IEntityTypeConfiguration<UserGameRole>
    {
        public void Configure(EntityTypeBuilder<UserGameRole> builder)
        {
            builder.ToTable("UserGameRoles");

            builder.HasKey(x => new { x.UserId, x.GameRoleId, x.SectionId });

            builder.HasOne(x => x.User)
                .WithMany(u => u.UserGameRoles)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.GameRole)
                .WithMany()
                .HasForeignKey(x => x.GameRoleId);

            //Relations
            builder.HasOne(x => x.SectionGame)
                .WithMany()
                .HasForeignKey(x => new { x.SectionId, x.GameId })
                .HasPrincipalKey(x => new { x.SectionId, x.GameId })
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
