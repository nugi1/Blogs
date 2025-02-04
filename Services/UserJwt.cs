using BlogsDAL.Interfaces;
using BlogsDAL.Models;

namespace Services;

public interface IUserJwtService
{
    Task<UserJwt> GetUser(string jwt);
}

public class UserJwtService(IUnitOfWork unitOfWork) : IUserJwtService
{
    public Task<UserJwt> GetUser(string jwt)
    {
        var userJwt = unitOfWork.UserJwtRepository.GetOneAsync(
            predicate: uj => uj.Jwt == jwt);

        return userJwt;
    }
}