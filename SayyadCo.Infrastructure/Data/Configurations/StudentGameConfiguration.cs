using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Infrastructure.Data.Configurations
{
    public class StudentGameConfiguration : BaseUserGameConfiguration<StudentGame>
    {
        public override void Configure(EntityTypeBuilder<StudentGame> builder)
        {
            base.Configure(builder);

            builder.ToTable("StudentGames");

            // Override relationship to specify the correct collection
            builder.HasOne(x => x.SectionGame)
                .WithMany(sg => sg.StudentGames)
                .HasForeignKey(x => new { x.SectionId, x.GameId })
                .HasPrincipalKey(sg => new { sg.SectionId, sg.GameId })
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
