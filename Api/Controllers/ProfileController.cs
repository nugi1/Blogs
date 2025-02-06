using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Services;
using Shared.Dtos;

namespace Api.Controllers;

public class ProfileDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int Age { get; set; } = 21;
    public string Avatar { get; set; } = "avatar.png";
}

public class ProfileBlogsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
}

[ApiController]
[Route("[controller]")]
public class ProfileController(IUserService userService, IBlogService blogService, IFollowService followService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ProfileDto>> GetProfile()
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (string.IsNullOrEmpty(token))
        {
            return BadRequest("Token is missing.");
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        
        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

        var profile = await userService.GetUserAsync(userIdClaim.Value);

        return Ok(new ProfileDto()
        {
            Id = Guid.Parse(profile.Id),
            Name = profile.UserName,
            Email = profile.Email
        });
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<ProfileDto>> GetProfile(Guid userId)
    {
        var profile = await userService.GetUserAsync(userId.ToString());

        return Ok(new ProfileDto()
        {
            Id = Guid.Parse(profile.Id),
            Name = profile.UserName,
            Email = profile.Email
        });
    }

    [HttpGet("friends/{userId}")]
    public async Task<ActionResult<IList<ProfileDto>>> GetMyFriends(Guid userId)
    {
        var ids = await followService.GetFollowings(userId);
        var users = await userService.GetUsersAsync(ids);

        return Ok(users.Select(u => new ProfileDto()
        {
            Id = Guid.Parse(u.Id),
            Name = u.UserName,
            Email = u.Email

        }).ToList());
    }

    [HttpGet("blogs/{userId}")]
    public async Task<ActionResult<IList<ProfileBlogsDto>>> GetBlogs(Guid userId)
    {
        var blogs = await blogService.GetBlogsByUserId(userId);

        return Ok(blogs.Select(b => new ProfileBlogsDto()
        {
            Id = b.Id,
            Text = b.Content,
            Title = b.Title
        }).ToList());
    }
}