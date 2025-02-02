using BlogsDAL.Interfaces;
using BlogsDAL.Models;

namespace BlogsDAL.Repositories;

public interface ICommentRepository : IGenericRepository<Comment>
{
    
}

public class CommentRepository(BlogDbContext context) : GenericRepository<Comment>(context), ICommentRepository
{
    
}