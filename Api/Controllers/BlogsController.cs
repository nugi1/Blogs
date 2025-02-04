using System.IdentityModel.Tokens.Jwt;
using BlogsDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Services;
using Shared.Dtos;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class BlogsController(IBlogService blogService, IUserService userService, ICommentService commentService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateBlog([FromBody] BlogDto blogDto)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (string.IsNullOrEmpty(token))
        {
            return BadRequest("Token is missing.");
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        
        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
        await blogService.CreateBlog(blogDto, userIdClaim.Value);
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