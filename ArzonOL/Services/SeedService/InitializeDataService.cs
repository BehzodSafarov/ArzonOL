using ArzonOL.Entities;
using Microsoft.AspNetCore.Identity;

namespace ArzonOL.Services.AuthService;

public class InitializeDataService
{
    public static async Task CreateDefaultRoles(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<InitializeDataService>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
     
        try
        {
            logger.LogInformation("Begined Adding default roles");
            var roles = config.GetSection("Identity:Roles").Get<List<string>>();
            
            foreach (var role in roles!)
            {
                logger.LogInformation("This role "+ role);
                try
                {
                if( await roleManager.FindByNameAsync(role) is null)
                await roleManager.CreateAsync(new IdentityRole(role));

                logger.LogInformation("Role created with name "+ role);
                }
                catch(Exception e)
                {
                logger.LogInformation("Failed Creating role with name "+ role);
                throw new Exception(e.Message);
                }
            }
        
        logger.LogInformation("Ended creating roles");
        roleManager.Dispose();
            
        }
        catch (System.Exception e)
        {
            
            throw new Exception(e.Message);
        }
    }

    public static async Task CreateDefaultAdmin(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<InitializeDataService>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

        var admin = config.GetSection("Identity:Admin").Get<SeedUser>();
        
        var newAdmin = new UserEntity
        {
           UserName = admin!.UserName,
           Email = admin.Email
        };

        if(newAdmin is null)
        {
            logger.LogInformation("Admin is not found frim apsettings");
            return;
        }
        
        try
        {
            var isExistAdmin = await userManager.CheckPasswordAsync(newAdmin, admin.Password!);
            var role = await roleManager.FindByNameAsync("Admin");

            if(!isExistAdmin && role is not null)
            {
                var createAdminResult =  await userManager.CreateAsync(newAdmin, admin.Password!);

                if(!createAdminResult.Succeeded)
                {
                    logger.LogInformation("Admin is not created with name "+  admin.UserName);
                    return;
                }

                var addToRoleResult = await userManager.AddToRoleAsync(newAdmin, role.Name!);

                if(!addToRoleResult.Succeeded)
                {
                    logger.LogInformation("Role is not created with name "+ role.Name);
                    return;
                }
            }
            
            logger.LogInformation("Admin Created Successefuly with name "+ admin.UserName);
        }
        catch(Exception e)
        {
           logger.LogInformation("Error with creating Seed Admin");
           throw new Exception(e.Message);
        }
    }
    public static async Task CreateDefaultUser(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<InitializeDataService>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

        var user = config.GetSection("Identity:User").Get<SeedUser>();
        
        var newUser = new UserEntity
        {
           UserName = user!.UserName,
           Email = user.Email
        };

        if(newUser is null)
        {
            logger.LogInformation("User is not found from apsettings");
            return;
        }
        
        try
        {
            var isExistAdmin = await userManager.CheckPasswordAsync(newUser, user.Password!);
            var role = await roleManager.FindByNameAsync("User");

            if(!isExistAdmin && role is not null)
            {
                var createUserResult =  await userManager.CreateAsync(newUser, user.Password!);

                if(!createUserResult.Succeeded)
                {
                    logger.LogInformation("User is not created with name "+ user.UserName);
                    return;
                }

                var addToRoleResult = await userManager.AddToRoleAsync(newUser, role.Name!);

                if(!addToRoleResult.Succeeded)
                {
                    logger.LogInformation("Role is not created with name "+ role.Name);
                    return;
                }
            }
            
            logger.LogInformation("User Created Successefuly with name"+ user.UserName);
        }
        catch(Exception e)
        {
           logger.LogInformation("Error with creating Seed User");
           throw new Exception(e.Message);
        }
    }

    public static async Task CreateDefaultMerchand(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<InitializeDataService>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

        var merchand = config.GetSection("Identity:Merchand").Get<SeedUser>();
        
        var newMerchand = new UserEntity
        {
           UserName = merchand!.UserName,
           Email = merchand.Email
        };

        if(newMerchand is null)
        {
            logger.LogInformation("Merchand is not found frim apsettings");
            return;
        }
        
        try
        {
            var isExistAdmin = await userManager.CheckPasswordAsync(newMerchand, merchand.Password!);
            var role = await roleManager.FindByNameAsync("Merchand");

            if(!isExistAdmin && role is not null)
            {
                var createMerchandResult =  await userManager.CreateAsync(newMerchand, merchand.Password!);

                if(!createMerchandResult.Succeeded)
                {
                    logger.LogInformation("Merchand is not created with name "+ merchand.UserName);
                    return;
                }

                var addToRoleResult = await userManager.AddToRoleAsync(newMerchand, role.Name!);

                if(!addToRoleResult.Succeeded)
                {
                    logger.LogInformation("Role is not created with name "+ role.Name);
                    return;
                }
            }
            
            logger.LogInformation("Merchand Created Successefuly with name "+ merchand.UserName);
        }
        catch(Exception e)
        {
           logger.LogInformation("Error with creating Seed Merchand");
           throw new Exception(e.Message);
        }
    }
}

public class SeedUser
{
    public string? UserName {get; set;}
    public string? Password {get; set;}
    public string? Email {get; set;}
}