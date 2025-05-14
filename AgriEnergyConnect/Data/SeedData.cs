using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnect.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Create roles if they don’t exist
            string[] roles = { "Farmer", "Employee" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Default Farmer user
            var farmerUserEmail = "farmer@farm.com";
            var farmerUser = await userManager.FindByEmailAsync(farmerUserEmail);
            if (farmerUser == null)
            {
                farmerUser = new IdentityUser
                {
                    UserName = farmerUserEmail,
                    Email = farmerUserEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(farmerUser, "Farmer123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(farmerUser, "Farmer");
                }
            }

            // Corresponding Farmer profile
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                var farmer = context.Farmers.FirstOrDefault(f => f.Email == farmerUser.Email);
                if (farmer == null)
                {
                    context.Farmers.Add(new Farmer
                    {
                        FarmerID = 1,
                        Name = "Test Farmer",
                        Email = farmerUser.Email,
                        Contact = "123-456-7890"
                    });
                    context.SaveChanges();
                }
            }

            // Default Employee user
            var employeeUserEmail = "admin@agrienergy.com";
            var employeeUser = await userManager.FindByEmailAsync(employeeUserEmail);
            if (employeeUser == null)
            {
                employeeUser = new IdentityUser
                {
                    UserName = employeeUserEmail,
                    Email = employeeUserEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(employeeUser, "Employee123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(employeeUser, "Employee");
                }
            }
        }
    }
}
