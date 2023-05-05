using Microsoft.AspNetCore.Identity;

namespace ArzonOL.Services.AuthService.Interfaces;

public interface IRegisterService
{
    Task<IdentityResult> RegisterAsync(string username, string password, string role, string email);
}