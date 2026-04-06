using SayyadCo.Domain.Common;
using SayyadCo.Domain.Enums;

namespace SayyadCo.Domain.Entities
{
    public class Plan : BaseEntity
    {
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public int DurationInDays { get; set; }
        public decimal Price { get; set; }
        public PlanType PlanType { get; set; }
    }
}
