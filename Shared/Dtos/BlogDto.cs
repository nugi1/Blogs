namespace Shared.Dtos;

public class BlogDto
{
    public DateTime PublishedAt { get; set; } = DateTime.Now.ToUniversalTime();

    public string Content { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
}