namespace BlogsDAL.Models;

public class Tag : Base
{
    public string Name { get; set; } = null!;
    public List<Post> Posts { get; set; } = null!;
}