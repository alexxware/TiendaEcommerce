using Microsoft.AspNetCore.Mvc;
using TiendaPuntoVenta.DTOs;
using TiendaPuntoVenta.Service;

namespace TiendaPuntoVenta.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] InsertUserDto user)
    {
        var res = await _userService.AddUser(user);
        if (res)
        {
            return Ok(new { Message = "User Added Successfully" });
        }
        return BadRequest();
    }
}