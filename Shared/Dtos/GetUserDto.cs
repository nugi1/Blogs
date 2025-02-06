namespace Shared.Dtos;

public class GetUserDto
{
    public string Id { get; set; }
    public string Username { get; set; }
    public int Age = 21;
    public string Email { get; set; }
    public string AvatarUrl { get; set; }
    public IList<GetPostDto> Blogs { get; set; }
}