namespace BlogsDAL.Models;

public class UserJwt : Base
{
    public Guid UserId { get; set; }
    public string Jwt { get; set; }
}