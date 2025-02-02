namespace BlogsDAL.Models;

public class Post : Base
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime PublishedAt { get; set; } = DateTime.Now.ToUniversalTime();
    public IList<Comment> Comments { get; set; } = new List<Comment>();
    public IList<Tag> Tags { get; set; } = new List<Tag>();
}