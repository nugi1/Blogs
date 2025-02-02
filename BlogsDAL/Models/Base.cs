namespace BlogsDAL.Models;

public class Base
{
    protected Base()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
}