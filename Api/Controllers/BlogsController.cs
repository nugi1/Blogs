using BlogsDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Services;
using Shared.Dtos;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class BlogsController(IBlogService blogService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateBlog([FromBody] BlogDto blogDto)
    {
        await blogService.CreateBlog(blogDto);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IList<Post>>> GetPostsByPublishDate()
    {
        var posts = await blogService.GetBlogsByPublishDate();

        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPostById(string id)
    {
        var guid = new Guid(id);
        
        var post = await blogService.GetPostBy(guid);
        
        return Ok(post);
    }
}