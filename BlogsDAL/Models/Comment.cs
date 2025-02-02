namespace BlogsDAL.Models;

public class Comment : Base
{
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public Guid PostId { get; set; }
    public Post Post { get; set; } = null!;
}