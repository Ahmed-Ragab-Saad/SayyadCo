namespace SayyadCo.Application.Features.SectionGames.Queries.GetSectionGamePlans
{
    public class GetSectionGamePlansResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string PlanId { get; set; } = string.Empty;
        public string PlanTitleEn { get; set; } = string.Empty;
        public string PlanTitleAr { get; set; } = string.Empty;
        public string GameRole { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
    }
}