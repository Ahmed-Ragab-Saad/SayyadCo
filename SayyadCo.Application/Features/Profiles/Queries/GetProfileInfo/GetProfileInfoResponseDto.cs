namespace SayyadCo.Application.Features.Profiles.Queries.GetProfileInfo
{
    public class GetProfileInfoResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}