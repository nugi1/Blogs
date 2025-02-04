namespace Shared.Dtos;

public class GetPostDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime PublishedAt { get; set; } = DateTime.Now.ToUniversalTime();
    public string UserId { get; set; }
    public string Username { get; set; }
    public List<CommentDto> Comments = new List<CommentDto>();
}