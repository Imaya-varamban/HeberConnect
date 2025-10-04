using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace YourAppNamespace.Data
{
    public static class DataSeeder
    {
        public static async Task SeedUsersAndRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Ensure roles
            string[] roles = { "Admin", "Staff", "Student" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create Admin
            var adminEmail = "admin@heberconnect.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            // Create Staff
            var staffEmail = "staff1@heberconnect.com";
            if (await userManager.FindByEmailAsync(staffEmail) == null)
            {
                var staff = new IdentityUser { UserName = staffEmail, Email = staffEmail, EmailConfirmed = true };
                await userManager.CreateAsync(staff, "Staff@123");
                await userManager.AddToRoleAsync(staff, "Staff");
            }

            // Create Students
            string[] studentEmails = {
                "student1@heberconnect.com",
                "student2@heberconnect.com",
                "student3@heberconnect.com",
                "student@test.com"
            };

            foreach (var studentEmail in studentEmails)
            {
                if (await userManager.FindByEmailAsync(studentEmail) == null)
                {
                    var student = new IdentityUser { UserName = studentEmail, Email = studentEmail, EmailConfirmed = true };
                    await userManager.CreateAsync(student, "Student@123");
                    await userManager.AddToRoleAsync(student, "Student");
                }
            }
        }
    }
}
