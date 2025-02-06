using BlogsDAL.Interfaces;
using BlogsDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogsDAL.Repositories;

public interface IPostRepository : IGenericRepository<Post>
{
    Task<IEnumerable<Post>> GetFollowerPostsAsync(Guid followerId);
}

public class PostRepository(BlogDbContext context) : GenericRepository<Post>(context), IPostRepository
{
    public async Task<IEnumerable<Post>> GetFollowerPostsAsync(Guid followerId)
    {
        var posts = await context.Posts.Where(p => p.UserId == followerId.ToString()).ToListAsync();

        return posts;
    }
}