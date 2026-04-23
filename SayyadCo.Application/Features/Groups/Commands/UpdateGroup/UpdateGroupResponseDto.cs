namespace SayyadCo.Application.Features.Groups.Commands.UpdateGroup
{
    public class UpdateGroupResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public bool IsPrivate { get; set; }
    }
}