using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TiendaPuntoVenta.DTOs;
using TiendaPuntoVenta.Service;

namespace TiendaPuntoVenta.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IValidator<InsertUserDto> _validatorInsertUser;

    public UserController(IUserService userService,  IValidator<InsertUserDto> validatorInsertUser)
    {
        _userService = userService;
        _validatorInsertUser = validatorInsertUser;
    }
    
    [HttpPost("Register")]
    public async Task<IActionResult> AddUser([FromBody] InsertUserDto user)
    {
        var validationResult = await _validatorInsertUser.ValidateAsync(user);
        if (validationResult.IsValid)
        {
            var res = await _userService.AddUser(user);
            if (res)
            {
                return Ok(new { Message = "User Added Successfully" });
            }
            return BadRequest();
        }
        
        return BadRequest(validationResult.Errors);
        
    }

}