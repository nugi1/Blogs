using BlogsDAL.Interfaces;
using BlogsDAL.Models;

namespace BlogsDAL.Repositories;

public interface IUserJwtRepository : IGenericRepository<UserJwt>
{
    
}

public class UserJwtRepository(BlogDbContext context) : GenericRepository<UserJwt>(context), IUserJwtRepository
{
    
}