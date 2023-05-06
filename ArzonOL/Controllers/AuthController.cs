using ArzonOL.Dtos.AuthDtos;
using ArzonOL.Services.AuthService.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ArzonOL.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILoginService _loginService;
    private readonly IRegisterService _registerService;
    private readonly PasswordValidator<IdentityUser> _passwordValidator;

    public AuthController(ILoginService loginService, IRegisterService registerService, PasswordValidator<IdentityUser> passwordValidator)
    {
        _loginService = loginService;
        _registerService = registerService;
        _passwordValidator = passwordValidator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginDto loginDto)
    {
        var token = await _loginService.LogInAsync(loginDto.UserName!, loginDto.Password!);

        if (string.IsNullOrEmpty(token))
            return BadRequest("Username or password is incorrect");

        return Ok(token);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterDto registerDto)
    {
        var validatePasswordResult = await _passwordValidator.ValidateAsync(null, null, registerDto.Password);

        if(!validatePasswordResult.Succeeded)
        return BadRequest(validatePasswordResult.Errors);
        
        var result = await _registerService.RegisterAsync(registerDto.UserName!, registerDto.Password!, "User", registerDto.Email!);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(result);
    }

    [HttpPost("merchant/register")]
    public async Task<IActionResult> RegisterMerchantAsync(RegisterDto registerDto)
    {
        var result = await _registerService.RegisterAsync(registerDto.UserName!, registerDto.Password!, "Merchand", registerDto.Email!);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(LoginDto loginDto)
    {
        var result = await _loginService.LogOutAsync(loginDto.UserName!, loginDto.Password!);
        
        if(!result.Succeeded)
           return BadRequest(result.Errors);
           
        return Ok(result);
    }
}