namespace SayyadCo.Application.Common.Models
{
    public class ValidationErrorResponse
    {
        public Dictionary<string, string[]> Errors { get; set; } = new();
    }
}
