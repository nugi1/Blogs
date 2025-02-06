using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Dtos;
using Shared.Exceptions;

namespace Services;

public interface IUserService
{
    Task<(SignInResult,string)> SignIn(SignInUserDto user);
    Task CreateUser(User user);
    Task<IdentityUser> GetBy(string id);
    Task<IList<IdentityUser>> GetUsersAsync(IList<Guid> followerIds);
    Task<IList<IdentityUser>> GetUsersAsync();
    Task<IdentityUser> GetUserAsync(string value);
}

public class UserService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration configuration) : IUserService
{
    public async Task<IdentityUser> GetBy(string id)
    {
        return await userManager.FindByIdAsync(id);
    }

    public async Task<IList<IdentityUser>> GetUsersAsync(IList<Guid> followerIds)
    {
        var result = new List<IdentityUser>();

        foreach (var followerId in followerIds)
        {
            var follower = await userManager.FindByIdAsync(followerId.ToString()) ??
                           throw new NotFoundException($"Follower with ID: {followerId} does not exist!");
            result.Add(follower);
        }

        return result;
    }

    public async Task<IList<IdentityUser>> GetUsersAsync()
    {
        return await userManager.Users.ToListAsync();
    }

    public async Task<IdentityUser> GetUserAsync(string value)
    {
        var user = await userManager.FindByIdAsync(value);

        return user;
    }

    public async Task<(SignInResult, string)> SignIn(SignInUserDto user)
    {
        var result = await signInManager.PasswordSignInAsync(user.Username, user.Password, false, false);
        var userId = await userManager.FindByNameAsync(user.Username);
        if (!result.Succeeded)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var token = GenerateJwtToken(user.Username, Guid.Parse(userId.Id));
        return (result, token);    
    }

    public async Task CreateUser(User user)
    {
        var identityUser = new IdentityUser
        {
            UserName = user.Username,
            Email = user.Email,
        };

        var result = await userManager.CreateAsync(identityUser, user.Password);

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
    
    public string GenerateJwtToken(string username, Guid userId)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, userId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.Now.AddMinutes(int.Parse(configuration["Jwt:ExpirationMinutes"]));

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}