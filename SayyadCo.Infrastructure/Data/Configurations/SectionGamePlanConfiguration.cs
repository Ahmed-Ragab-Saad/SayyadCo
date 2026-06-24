using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Infrastructure.Data.Configurations
{
    public class SectionGamePlanConfiguration : IEntityTypeConfiguration<SectionGamePlan>
    {
        public void Configure(EntityTypeBuilder<SectionGamePlan> builder)
        {
            builder.ToTable("SectionGamePlans");
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.PlanId, x.SectionId, x.GameId, x.GameRoleId })
                .IsUnique();

            builder.HasOne(x => x.Plan)
                .WithMany(p => p.SectionGamePlans)
                .HasForeignKey(x => x.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.SectionGame)
                .WithMany(sg => sg.SectionGamePlans)
                .HasForeignKey(x => new { x.SectionId, x.GameId })
                .HasPrincipalKey(sg => new { sg.SectionId, sg.GameId })
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.GameRole)
                .WithMany(sg => sg.SectionGamePlans)
                .HasForeignKey(x => x.GameRoleId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
