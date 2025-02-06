using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogsDAL.Interfaces;
using BlogsDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Dtos;

namespace Services;

public interface IBlogService
{
    public Task CreateBlog(BlogDto blogDto, string userId);
    Task<IList<Post>> GetBlogsByPublishDate();
    Task<Post> GetPostBy(Guid id);
    Task<IList<Post>> GetFollowerPostsByDatetimeAsync(IList<Guid> followerIds);
    Task<IList<GetPostDto>> GetBlogsByUserId(Guid userId);
    Task<IList<GetPostDto>> GetBlogsByFollowings(IList<Guid> followingIds);
}

public class BlogService(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager) : IBlogService
{
    public async Task CreateBlog(BlogDto blogDto, string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        Post post = new Post()
        {
            PublishedAt = blogDto.PublishedAt,
            Title = blogDto.Title,
            Content = blogDto.Content,
            Username = user.UserName,
            UserId = userId
        };
        
        await unitOfWork.PostRepository.AddAsync(post);
        await unitOfWork.SaveAsync();
    }

    public async Task<IList<Post>> GetBlogsByPublishDate()
    {
        var blogs = await unitOfWork.PostRepository.GetAsync();

        return blogs.OrderByDescending(b => b.PublishedAt).ToList();
    }
    
    public async Task<Post> GetPostBy(Guid id)
    {
        var post = await unitOfWork.PostRepository.GetByIdAsync(id);

        return post;
    }

    public async Task<IList<Post>> GetFollowerPostsByDatetimeAsync(IList<Guid> followerIds)
    {
        var result = new List<Post>();
        foreach (var followerId in followerIds)
        {
            var posts = await unitOfWork.PostRepository.GetFollowerPostsAsync(followerId);

            result.AddRange(posts);
        }

        return result;
    }

    public async Task<IList<GetPostDto>> GetBlogsByUserId(Guid userId)
    {
        var r = await unitOfWork.PostRepository.GetAsync(
            predicate: p => p.UserId == userId.ToString(),
            include: b => b.Include(e => e.Comments));

        return r.Select(e => new GetPostDto()
        {
            Username = e.Username,
            UserId = e.UserId,
            PublishedAt = e.PublishedAt,
            Id = e.Id,
            Title = e.Title,
            Comments = e.Comments.Select(c => new CommentDto()
            {
                Author = c.Author,
                Text = c.Content
            }).ToList()
        }).ToList();
    }

    public async Task<IList<GetPostDto>> GetBlogsByFollowings(IList<Guid> followingIds)
    {
        var result = new List<GetPostDto>();
        foreach (var id in followingIds)
        {
            var blogs = (await unitOfWork.PostRepository
                .GetAsync(predicate: e => e.UserId == id.ToString(), orderBy: e => e.OrderByDescending(b => b.PublishedAt), include: e => e.Include(c => c.Comments)))
                .Select(e => new GetPostDto()
                {
                    Id = e.Id,
                    PublishedAt = e.PublishedAt,
                    Title = e.Title,
                    Content = e.Content,
                    Username = e.Username,
                    UserId = e.UserId,
                    Comments = e.Comments.Select(c => new CommentDto()
                    {
                        Author = c.Author,
                        Text = c.Content
                    }).ToList()
                }).ToList();
            foreach (var blog in blogs)
            {
                var comments = await unitOfWork.CommentRepository.GetAsync(
                    predicate: c => c.PostId == blog.Id);
                blog.Comments = comments.Select(c => new CommentDto()
                {
                    Author = c.Author,
                    Text = c.Content
                }).ToList();
            }
            result.AddRange(blogs);
        }

        return result;
    }
}