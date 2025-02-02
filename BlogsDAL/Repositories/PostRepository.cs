using BlogsDAL.Interfaces;
using BlogsDAL.Models;

namespace BlogsDAL.Repositories;

public interface IPostRepository : IGenericRepository<Post>
{
    
}

public class PostRepository(BlogDbContext context) : GenericRepository<Post>(context), IPostRepository
{
    
}