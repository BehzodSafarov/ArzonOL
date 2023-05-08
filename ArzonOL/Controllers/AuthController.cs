using ArzonOL.Dtos.AuthDtos;
using ArzonOL.Enums;
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

    public AuthController(ILoginService loginService, IRegisterService registerService)
    {
        _loginService = loginService;
        _registerService = registerService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginDto loginDto)
    {
        try
        {
            var token = await _loginService.LogInAsync(loginDto.UserName!, loginDto.Password!);

            if (string.IsNullOrEmpty(token))
                return BadRequest("Username or password is incorrect");

            return Ok(token);
        }
        catch (System.Exception e)
        {
            
            throw new Exception(e.Message);
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterDto registerDto)
    {   
        try
        {
            var result = await _registerService.RegisterAsync(registerDto.UserName!, registerDto.Password!, "User", registerDto.Email!);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result);
        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpPost("merchant/register")]
    public async Task<IActionResult> RegisterMerchantAsync(RegisterDto registerDto)
    {
        try
        {
            var result = await _registerService.RegisterAsync(registerDto.UserName!, registerDto.Password!, "Merchand", registerDto.Email!);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result.Succeeded);
        }
        catch (System.Exception e )
        {
            throw new Exception(e.Message);
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(LogOutDto loginDto)
    {
        try
        {
            var result = await _loginService.LogOutAsync(loginDto.Id, loginDto.Password!);
            
            if(!result.Succeeded)
            return BadRequest(result.Errors);
            
            return Ok(result.Succeeded);
        }
        catch (System.Exception e)
        {
            
            throw new Exception(e.Message);
        }
    }

    [HttpPost("changePassword")]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
    {
      try
      {
        if(Guid.Empty == changePasswordDto.Id)
        return BadRequest(IdentityResult.Failed(new IdentityError{Code = EErrorType.ServerError.ToString(), Description = "Id can't be null here"}));
        
        return Ok(await _registerService.ChangePasswordAsync(changePasswordDto.Id, changePasswordDto.OldPassword!, changePasswordDto.NewPassword!));
      }
      catch (System.Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    [HttpPost("changeUsername")]
    public async Task<IActionResult> ChangeUserName(ChangeUsernameDto changeUsernameDto)
    {
        try
        {
            var result = await _registerService.ChangeUsernameAsync(changeUsernameDto.Id, changeUsernameDto.NewUserName!);

            if(!result.Succeeded)
            return BadRequest(result.Errors);

            return Ok(result.Succeeded);
        }
        catch (System.Exception e)
        {
            
            throw new Exception(e.Message);
        }
    }

}