using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FollowController(IFollowService followService, IUserService userService) : ControllerBase
{
    [HttpPost("{followerId}")]
    public async Task<ActionResult> AddFollowerAsync(Guid followerId)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (string.IsNullOrEmpty(token))
        {
            return BadRequest("Token is missing.");
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        
        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
        await followService.Follow(Guid.Parse(userIdClaim.Value), followerId);

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult> GetFollowers(Guid userId)
    {
        var followerIds = await followService.GetFollowersAsync(userId);
        var users = await userService.GetUsersAsync(followerIds);

        return Ok(users);
    }
}