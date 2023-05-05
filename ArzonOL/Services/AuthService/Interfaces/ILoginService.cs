using Microsoft.AspNetCore.Identity;
namespace ArzonOL.Services.AuthService.Interfaces;

public interface ILoginService
{
    Task<string> LogInAsync(string username, string password);
    Task<IdentityResult> LogOutAsync(string username, string password);
    string CreateJwtToken(string username, string password, string role);
    bool ValidateJwtToken(string token);
}