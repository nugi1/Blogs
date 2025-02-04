using BlogsDAL.Interfaces;
using BlogsDAL.Models;
using Microsoft.AspNetCore.Identity;
using Shared.Dtos;

namespace Services;

public interface ICommentService
{
    Task AddComment(Guid postId, string userId, CommentDto commentDto);

    Task<List<CommentDto>> GetComments(Guid postId);
}

public class CommentService(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager) : ICommentService
{
    public async Task AddComment(Guid postId, string userId, CommentDto commentDto)
    {
        var user = await userManager.FindByIdAsync(userId);
        unitOfWork.CommentRepository.AddAsync(new Comment()
        {
            PostId = postId,
            Author = user.UserName,
            UserId = user.Id,
            CreatedAt = DateTime.Now.ToUniversalTime(),
            Content = commentDto.Text
        });

        await unitOfWork.SaveAsync();
    }

    public async Task<List<CommentDto>> GetComments(Guid postId)
    {
        return (await unitOfWork.CommentRepository.GetAsync(
            predicate: c => c.PostId == postId)).Select(c => new CommentDto()
        {
            Author = c.Author,
            Text = c.Content
        }).ToList();
    }
}