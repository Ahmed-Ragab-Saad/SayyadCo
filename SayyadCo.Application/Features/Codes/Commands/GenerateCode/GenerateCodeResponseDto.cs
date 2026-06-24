using SayyadCo.Domain.Enums;

namespace SayyadCo.Application.Features.Codes.Commands.GenerateCode
{
    public class GenerateCodeResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;
        public string PlanId { get; set; } = string.Empty;
        public string PlanTitleEn { get; set; } = string.Empty;
        public string GameRole { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public PlanType PlanType { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}