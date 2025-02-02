using BlogsDAL.Interfaces;
using BlogsDAL.Models;

namespace BlogsDAL.Repositories;

public interface ITagRepository : IGenericRepository<Tag>
{
    
}

public class TagRepository(BlogDbContext context) : GenericRepository<Tag>(context), ITagRepository
{
    
}