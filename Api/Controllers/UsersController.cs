using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services;
using Shared.Dtos;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService, IBlogService blogService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<GetUserDto>> GetUsersAsync()
    {
        var users = await userService.GetUsersAsync();

        var result = new List<GetUserDto>();

        foreach (var user in users)
        {
            var blogs = await blogService.GetBlogsByUserId(Guid.Parse(user.Id));
            result.Add(new GetUserDto()
            {
                Username = user.UserName,
                Email = user.Email,
                AvatarUrl = "avatar.png",
                Age = 21,
                Id = user.Id.ToString(),
                Blogs = blogs
            });
        }

        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] Shared.Dtos.User user)
    {
        await userService.CreateUser(user);
        return Ok();
    }
    
    [HttpPost("sign-in")]
    public async Task<ActionResult<object>> SignIn([FromBody] Shared.Dtos.SignInUserDto user)
    {
        try
        {
            var (result, token) = await userService.SignIn(user);
            if (result.Succeeded)
            {
                var response = new { success = true };
                Console.WriteLine("Backend Response: " + JsonConvert.SerializeObject(response)); // Log response
                return Ok(new {success = true, token = token});
            }

            var failureResponse = new { success = false };
            Console.WriteLine("Backend Response: " + JsonConvert.SerializeObject(failureResponse)); // Log failure response
            return Ok(new { success = true, token = result });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid email or password.");
        }
        catch (Exception ex)
        {
            // Catch any other exceptions and log them
            Console.WriteLine("Error during sign-in: " + ex.Message);
            return StatusCode(500, "Internal server error");
        }
    }
}