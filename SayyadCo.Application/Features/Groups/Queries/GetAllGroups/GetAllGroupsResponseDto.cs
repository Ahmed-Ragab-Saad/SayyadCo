namespace SayyadCo.Application.Features.Groups.Queries
{
    public class GetAllGroupsResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public bool IsPrivate { get; set; } = false;
    }
}
