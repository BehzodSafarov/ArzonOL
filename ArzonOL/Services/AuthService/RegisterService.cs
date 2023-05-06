using ArzonOL.Entities;
using ArzonOL.Services.AuthService.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ArzonOL.Services.AuthService;

public class RegisterService : IRegisterService
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly ILogger<RegisterService> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;    

    public RegisterService(UserManager<UserEntity> userManager,
                           SignInManager<UserEntity> signInManager,
                           ILogger<RegisterService> logger,
                           RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _roleManager = roleManager;
    }

    public async Task<IdentityResult> ChangePasswordAsync(string username, string oldPassword, string newPassword)
    {
        _logger.LogInformation("Changing password");
        
        try
        {
            var existUser = await _userManager.FindByNameAsync(username);
            
            if(existUser is null)
            return IdentityResult.Failed(new IdentityError{Code = "PasswordChange", Description = "This user didn't not found"});

            var checkedPassword = await _userManager.CheckPasswordAsync(existUser!, oldPassword);

            if(checkedPassword)
            return IdentityResult.Failed(new IdentityError{Code = "PasswordChange", Description = "Your passwor is not found"});
            
            var changetPasswordResult = await _userManager.ChangePasswordAsync(existUser, oldPassword, newPassword);

            if(!changetPasswordResult.Succeeded)
             return IdentityResult.Failed(new IdentityError{Code = "PasswordChange", Description = changetPasswordResult.Errors.ToString()!});

             return IdentityResult.Success;
        }
        catch(Exception e)
        {
            _logger.LogInformation(e.Message);
            throw new Exception(e.Message);
        }
    }

    public async Task<IdentityResult> RegisterAsync(string username, string password, string role, string email)
    {
        _logger.LogInformation("Registering user {username}", username);

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
        return IdentityResult.Failed(new IdentityError {Code = "Register", Description = "Username or password is empty" });
        
        try
        {

            var userCreateResult = await _userManager.CreateAsync(new UserEntity { UserName = username, Email = email }, password);
            
            if (!userCreateResult.Succeeded)
            return IdentityResult.Failed(new IdentityError {Code = "Register", Description = "Bu username allaqachon foydalanilmoqda" });

            var user = await _userManager.FindByNameAsync(username);

            var roleExists = await _roleManager.RoleExistsAsync(role);

            if(roleExists && user is not null)
             await _userManager.AddToRoleAsync(user, role);

            return IdentityResult.Success;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to check if role exists");
            return IdentityResult.Failed(new IdentityError { Description = "Failed to check if role exists" });
        }
        
    }
}