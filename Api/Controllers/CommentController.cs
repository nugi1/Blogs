using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Services;
using Shared.Dtos;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController(ICommentService commentService) : ControllerBase
{
    [HttpPost("{postId}")]
    public async Task<IActionResult> AddComment(Guid postId, [FromBody] CommentDto commentDto)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (string.IsNullOrEmpty(token))
        {
            return BadRequest("Token is missing.");
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        
        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

        await commentService.AddComment(postId, userIdClaim.Value, commentDto);

        return Ok();
    }

    [HttpGet("{postId}")]
    public async Task<ActionResult<List<GetPostDto>>> GetPosts(Guid postId)
    {
        var comments = await commentService.GetComments(postId);

        return Ok(comments);
    }
}