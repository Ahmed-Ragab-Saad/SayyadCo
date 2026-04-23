using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Infrastructure.Data.Configurations
{
    public class TeacherGameConfiguration : BaseUserGameConfiguration<TeacherGame>
    {
        public override void Configure(EntityTypeBuilder<TeacherGame> builder)
        {
            base.Configure(builder);

            builder.ToTable("TeacherGames");

            // Override relationship to specify the correct collection
            builder.HasOne(x => x.SectionGame)
                .WithMany(sg => sg.TeacherGames)
                .HasForeignKey(x => new { x.SectionId, x.GameId })
                .HasPrincipalKey(sg => new { sg.SectionId, sg.GameId })
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
