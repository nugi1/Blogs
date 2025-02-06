using BlogsDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogsDAL.Repositories;

public interface IUserFollowerRepository
{
    Task AddAsync(Guid userId, Guid followerId);
    void Remove(Guid userId, Guid followerId);
    Task<IList<Guid>> GetFollowerIds(Guid userId);
    Task<IList<Guid>> GetFollowingIds(Guid userId);
}

public class UserFollowerRepository(BlogDbContext context) : IUserFollowerRepository
{
    public async Task AddAsync(Guid userId, Guid followerId)
    {
        var entity = new UserFollower()
        {
            FollowerId = userId,
            UserId = followerId
        };
        
        await context.UserFollowers.AddAsync(entity);
    }

    public void Remove(Guid userId, Guid followerId)
    {
        var entity = new UserFollower()
        {
            UserId = userId,
            FollowerId = followerId
        };

        context.Remove(entity);
    }

    public async Task<IList<Guid>> GetFollowerIds(Guid userId)
    {
        var followerIds = await context.UserFollowers
            .Where(uf => uf.UserId == userId)
            .Select(uf => uf.FollowerId)
            .ToListAsync();

        return followerIds;
    }

    public async Task<IList<Guid>> GetFollowingIds(Guid userId)
    {
        var followingIds = await context.UserFollowers.Where(uf => uf.FollowerId == userId).Select(uf => uf.UserId).ToListAsync();

        return followingIds;
    }
}