using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
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