using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Dtos;

namespace Services;

public interface IUserService
{
    Task<(SignInResult,string)> SignIn(SignInUserDto user);
    Task CreateUser(User user);
}

public class UserService(SignInManager<IdentityUser> signInManager ,UserManager<IdentityUser> userManager, IConfiguration configuration) : IUserService
{
    public async Task<(SignInResult, string)> SignIn(SignInUserDto user)
    {
        var result = await signInManager.PasswordSignInAsync(user.Username, user.Password, false, false);

        if (!result.Succeeded)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var token = GenerateJwtToken(user.Username);
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
    
    public string GenerateJwtToken(string username)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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