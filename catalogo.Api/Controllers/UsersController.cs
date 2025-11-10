using catalogo.Application.Services;
using catalogo.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace catalogo.Api.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetById(id);
        if  (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, User user)
    {
        user.Id = id;
        var updateUser = await _userService.Update(user);
        if (updateUser == null) return NotFound();
        return Ok(updateUser);
    }

    [HttpDelete]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteUser = await _userService.Delete(id);
        if (deleteUser == null) return NotFound();
        return Ok(deleteUser);
    }
}