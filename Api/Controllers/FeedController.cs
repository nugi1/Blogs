using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Services;
using Shared.Dtos;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FeedController(IFollowService followService, IBlogService blogService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetPostDto>>> GetFeed()
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (string.IsNullOrEmpty(token))
        {
            return BadRequest("Token is missing.");
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        
        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
        var followingIds = await followService.GetFollowings(Guid.Parse(userIdClaim.Value));
        var posts = await blogService.GetBlogsByFollowings(followingIds);

        return Ok(posts);
    }
}