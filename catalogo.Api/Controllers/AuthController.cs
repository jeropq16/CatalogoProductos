using catalogo.Application.Services;
using catalogo.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace catalogo.Api.Controllers;


[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(User user)
    {
        var CreateUser = await _authService.Register(user);
        if (CreateUser == null) return BadRequest();
        return Ok(CreateUser);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(User user)
    {
        var token  = await _authService.Login(user.Email, user.Password);
        if (token == null) return Unauthorized();
        return Ok(token);
    }
    
    public record LoginRequest(string Email, string Password);
    
}