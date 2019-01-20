using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreTodo
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager);

            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            await EnsureTestAdminAsync(userManager);
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            // Create the Administrator role if it doesn't already exist
            if (!(await roleManager.RoleExistsAsync(Constants.ADMINISTRATOR_ROLE)))
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.ADMINISTRATOR_ROLE));
            }
        }

        private static async Task EnsureTestAdminAsync(UserManager<IdentityUser> userManager)
        {
            // Create the default admin account and apply the Administrator role
            const string TEST_ADMIN_USER = "admin@todo.local";
            const string TEST_ADMIN_USER_PASSWORD = "NotSecure123!";

            var testAdmin = await userManager.Users
                .Where(x => x.UserName == TEST_ADMIN_USER)
                .SingleOrDefaultAsync();

            if (testAdmin == null)
            {
                testAdmin = new IdentityUser
                {
                    UserName = TEST_ADMIN_USER,
                    Email = TEST_ADMIN_USER
                };

                var success = await userManager.CreateAsync(testAdmin, TEST_ADMIN_USER_PASSWORD);
                if (success.Succeeded)
                {
                    await userManager.AddToRoleAsync(testAdmin, Constants.ADMINISTRATOR_ROLE);
                }
            }
        }
    }
}