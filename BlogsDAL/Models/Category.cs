namespace BlogsDAL.Models;

public class Category : Base
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IList<Post> Posts { get; set; } = null!;
}