namespace ArzonOL.Services.AuthService.Interfaces;

public interface ILoginService
{
    ValueTask<string> LogInAsync(string username, string password);
    Task LogOutAsync(string username, string password);
    string CreateJwtToken(string username, string password, string role);
    bool ValidateJwtToken(string token);
}