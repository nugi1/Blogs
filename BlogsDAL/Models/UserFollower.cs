namespace BlogsDAL.Models;

public class UserFollower
{
    public Guid UserId { get; set; }
    public Guid FollowerId { get; set; }
}