namespace SayyadCo.Application.Common.Models
{
    public class RegisterResultModel
    {
        public bool Succeeded { get; set; }
        public string UserId { get; set; } = string.Empty;
        public IEnumerable<string> Errors { get; set; } = [];
    }
}
