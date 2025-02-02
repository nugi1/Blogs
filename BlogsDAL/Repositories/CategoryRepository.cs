using BlogsDAL.Models;
using BlogsDAL.Repositories;

namespace BlogsDAL.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    
}

public class CategoryRepository(BlogDbContext context) : GenericRepository<Category>(context), ICategoryRepository
{
    
}