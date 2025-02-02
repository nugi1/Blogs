using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogsDAL.Interfaces;
using BlogsDAL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Dtos;

namespace Services;

public interface IBlogService
{
    public Task CreateBlog(BlogDto blogDto);
    Task<IList<Post>> GetBlogsByPublishDate();
    Task<Post> GetPostBy(Guid id);
}

public class BlogService(IUnitOfWork unitOfWork) : IBlogService
{
    public async Task CreateBlog(BlogDto blogDto)
    {
        Post post = new Post()
        {
            PublishedAt = blogDto.PublishedAt,
            Title = blogDto.Title,
            Content = blogDto.Content
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
}