using ArzonOL.Entities;
using ArzonOL.Services.AuthService.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ArzonOL.Services.AuthService;


public class LoginService : ILoginService
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly ILogger<LoginService> _logger;
    private readonly IConfiguration _configuration;

    public LoginService(UserManager<UserEntity> userManager,
                        SignInManager<UserEntity> signInManager,
                        ILogger<LoginService> logger,
                        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _configuration = configuration;
    }

    public string CreateJwtToken(string username, string password, string role)
    {
       _logger.LogInformation("Creating JWT token for user {username}", username);

       if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
       {
           _logger.LogWarning("Username or password is empty");
            return string.Empty;
       }

       var claims = new List<Claim>
       {
           new Claim(ClaimTypes.Name, username),
           new Claim("Password", password),
           new Claim(ClaimTypes.Role, role)
       };
       
       var tokenHandler = new JwtSecurityTokenHandler();
       var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);

       var tokenDescriptor = new SecurityTokenDescriptor
       {
           Subject = new ClaimsIdentity(claims),
           Expires = DateTime.UtcNow.AddMinutes(10),
           SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                    SecurityAlgorithms.HmacSha256Signature)
       };

       return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }

    public ValueTask<string> LogInAsync(string username, string password)
    {
        _logger.LogInformation("Logging in user {username}", username);

        if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            _logger.LogWarning("Username or password is empty");
            return new ValueTask<string>(string.Empty);
        }

        var identityUser = _userManager.CheckPasswordAsync(new UserEntity { UserName = username }, password);

        if(identityUser.Result == false)
        {
            _logger.LogWarning("Username or password is incorrect");
            return new ValueTask<string>(string.Empty);
        }

        var user = _userManager.FindByLoginAsync(username, password);

        if(user.Result == null)
        {
            _logger.LogWarning("User not found in database");
            return new ValueTask<string>(string.Empty);
        }

        var roles = _userManager.GetRolesAsync(user.Result);
        
        if(roles.Result.Count == 0)
        {
            _logger.LogWarning("User has no roles");
            return new ValueTask<string>(string.Empty);
        }

        return new ValueTask<string>(CreateJwtToken(username, password, roles.Result[0]));
    }

    public Task LogOutAsync(string username, string password)
    {
        _logger.LogInformation("Logging out user {username}", username);

        if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            _logger.LogWarning("Username or password is empty");
            return null!;
        }

        var identityUser = _userManager.FindByLoginAsync(username, password);

        if(identityUser.Result == null)
        {
            _logger.LogWarning("User not found in database");
            return null!;
        }
    }

    public bool ValidateJwtToken(string token)
    {
        _logger.LogInformation("Validating JWT token");
        if(string.IsNullOrEmpty(token))
        {
            _logger.LogWarning("Token is empty");
            return false;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return true;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Token validation failed");
            return false;
        }
    }
}