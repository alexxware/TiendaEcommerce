using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaPuntoVenta.DTOs;
using TiendaPuntoVenta.Service;
using TiendaPuntoVenta.Service.Auth;

namespace TiendaPuntoVenta.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IValidator<InsertUserDto> _validatorInsertUser;
    private readonly IValidator<LoginUserDto> _validatorLoginUser;

    public UserController(
        IUserService userService,  
        IValidator<InsertUserDto> validatorInsertUser,
        IValidator<LoginUserDto> validatorLoginUser)
    {
        _userService = userService;
        _validatorInsertUser = validatorInsertUser;
        _validatorLoginUser = validatorLoginUser;
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
    
    [HttpPost("Login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserDto user)
    {
        var validationResult = await _validatorLoginUser.ValidateAsync(user);
        if (validationResult.IsValid)
        {
            var res = await _userService.ValidateUser(user);
            if (!string.IsNullOrEmpty(res.Token))
            {
                //var token = _jwtHelper.GenerateToken
                return Ok(new { Message = "Login Successfully", Token = res.Token });
            }
            return Unauthorized("Username or password is incorrect");
        }

        return BadRequest(validationResult.Errors);
    }

    [HttpPost("LoginAdmin")]
    public async Task<IActionResult> LoginAdmin([FromBody] LoginUserDto user)
    {
        var validationResult = await _validatorLoginUser.ValidateAsync(user);
        if (validationResult.IsValid)
        {
            var res = await _userService.ValidateAdmin(user);
            if (string.IsNullOrEmpty(res.Token)) return Unauthorized("Username or password is incorrect");
            return Ok(new { Message = "Login Successfully", Token = res.Token });
        }

        return BadRequest(validationResult.Errors);
    }

    /*
    [Authorize]
    [HttpGet("privado")]
    public IActionResult Privado()
    {
        return Ok("Solo usuarios con JWT valido pueden ver esto");
    }*/

}