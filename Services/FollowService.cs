using BlogsDAL.Interfaces;
using BlogsDAL.Repositories;

namespace Services;

public interface IFollowService
{
    Task Follow(Guid userId, Guid followerId);
    Task<IList<Guid>> GetFollowersAsync(Guid userId);
    Task<IList<Guid>> GetFollowings(Guid userId);
}

public class FollowService(IUnitOfWork unitOfWork) : IFollowService
{
    public async Task Follow(Guid userId, Guid followerId)
    {
        await unitOfWork.UserFollowerRepository.AddAsync(userId, followerId);
        await unitOfWork.SaveAsync();
    }

    public async Task<IList<Guid>> GetFollowersAsync(Guid userId)
    {
        var followerIds = await unitOfWork.UserFollowerRepository.GetFollowerIds(userId);

        return followerIds;
    }

    public async Task<IList<Guid>> GetFollowings(Guid userId)
    {
        var ids = await unitOfWork.UserFollowerRepository.GetFollowingIds(userId);

        return ids;
    }
}