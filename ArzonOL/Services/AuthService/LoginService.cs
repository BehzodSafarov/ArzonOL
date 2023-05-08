using ArzonOL.Entities;
using ArzonOL.Enums;
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

    public string CreateJwtToken(string username, string password, string email, string role)
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
           new Claim(ClaimTypes.Role, role),
           new Claim(ClaimTypes.Email, email)
       };
       
       var tokenHandler = new JwtSecurityTokenHandler();

       var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
       var issuer = _configuration.GetSection("Jwt")["Issuer"];
       var audience = _configuration.GetSection("Jwt")["Audience"];

       var tokenDescriptor = new SecurityTokenDescriptor
       {
           Issuer = issuer,
           Audience = audience,
           Subject = new ClaimsIdentity(claims),
           Expires = DateTime.UtcNow.AddMinutes(10),
           SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                    SecurityAlgorithms.HmacSha256Signature)
       };

       return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }

    public async Task<string> LogInAsync(string username, string password)
    {
        _logger.LogInformation("Logging in user {username}", username);

        if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            _logger.LogWarning("Username or password is empty");
            return string.Empty;
        }

        var existUser = await _userManager.FindByNameAsync(username);
        
        if(existUser is null)
        {
            _logger.LogInformation("User didn't found");
            return string.Empty;
        }

        var identityUser = await _userManager.CheckPasswordAsync(existUser!, password);

        if(identityUser == false)    
        {
            _logger.LogWarning("Username or password is incorrect");
            return string.Empty;
        }

        var user = await _userManager.FindByNameAsync(username);

        if(user == null)
        {
            _logger.LogWarning("User not found in database");
            return string.Empty;
        }

        var roles = await _userManager.GetRolesAsync(user);
        
        if(roles.Count == 0)
        {
            _logger.LogWarning("User has no roles");
            return string.Empty;
        }

        var result =  CreateJwtToken(username, password, user.Email!, roles[0]);

        if(string.IsNullOrEmpty(result))
        return IdentityResult.Failed(new IdentityError{Code = EErrorType.ServerError.ToString(), Description = "Aniything went wrong"}).ToString();

        return result;
    }

    public async Task<IdentityResult> LogOutAsync(Guid id, string password)
    {
        _logger.LogInformation("Logging out user with id"+ id.ToString());

        if(Guid.Empty == id)
        {
            _logger.LogWarning("password is empty");
            return IdentityResult.Failed(new IdentityError{Code = EErrorType.ClientError.ToString(), Description = "Username yoki password bo'sh"});
        }

        if(Guid.Empty == id)
        {
            _logger.LogWarning("Username or password is empty");
            return IdentityResult.Failed(new IdentityError{Code = EErrorType.ServerError.ToString(), Description = "Id can't be null here"});
        }
        
        try
        {
            var identityUser = await _userManager.FindByIdAsync(id.ToString());
            
            if(identityUser is null)
            return IdentityResult.Failed(new IdentityError{Code = EErrorType.ClientError.ToString(), Description = "Bunday user topilmadi"});

            await _userManager.DeleteAsync(identityUser);
            
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User {password} logged out", password);

            return IdentityResult.Success;
        }
        catch(Exception e)
        {
          _logger.LogInformation(e.Message);
          throw new Exception(e.Message);
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
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            if(validatedToken == null)
            {
                _logger.LogWarning("Token validation failed");
                return false;
            }

            _logger.LogInformation("Token validation successful");
            return true;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Token validation failed");
            return false;
        }
    }
}