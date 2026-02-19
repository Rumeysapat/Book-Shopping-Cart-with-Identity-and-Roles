using System;
using BookShoppingCardUI.Constants;
using Microsoft.AspNetCore.Identity;

namespace BookShoppingCardUI.Data;

public class DbSeeder
{



    public static async Task SeedDefaultData(IServiceProvider servise)
    {
        var userMgr = servise.GetService<UserManager<IdentityUser>>();
        var roleMgr = servise.GetService<RoleManager<IdentityRole>>();

        await roleMgr.CreateAsync(new IdentityRole(RoleNames.Admin.ToString()));
        await roleMgr.CreateAsync(new IdentityRole(RoleNames.User.ToString()));

        //create admin user

        var admin = new IdentityUser
        {
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com",
            EmailConfirmed = true

        };

        var userInDb = await userMgr.FindByEmailAsync(admin.Email);

        if (userInDb is null)
        {
            await userMgr.CreateAsync(admin, "Admin@123");
            await userMgr.AddToRolesAsync(admin, new[] { RoleNames.Admin });




        }
    }
}