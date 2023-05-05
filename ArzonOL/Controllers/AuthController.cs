using ArzonOL.Services.AuthService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArzonOL.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILoginService _loginService;
    private readonly IRegisterService _registerService;

    public AuthController(ILoginService loginService, IRegisterService registerService)
    {
        _loginService = loginService;
        _registerService = registerService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(string username, string password)
    {
        var token = await _loginService.LogInAsync(username, password);

        if (string.IsNullOrEmpty(token))
            return BadRequest("Username or password is incorrect");

        return Ok(token);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(string username, string password, string email)
    {
        var result = await _registerService.RegisterAsync(username, password, "User", email);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok();
    }

    [HttpPost("merchant/register")]
    public async Task<IActionResult> RegisterMerchantAsync(string username, string password, string email)
    {
        var result = await _registerService.RegisterAsync(username, password, "Merchand", email);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(string username, string password)
    {
        var result = await _loginService.LogOutAsync(username, password);
        
        if(!result.Succeeded)
           return BadRequest(result.Errors);
           
        return Ok(result);
    }
}