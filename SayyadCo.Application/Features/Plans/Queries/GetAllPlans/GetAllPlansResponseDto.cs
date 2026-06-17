namespace SayyadCo.Application.Features.Plans.Queries.GetAllPlans
{
    public class GetAllPlansResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public int DurationInDays { get; set; }
        public decimal Price { get; set; }
    }
}