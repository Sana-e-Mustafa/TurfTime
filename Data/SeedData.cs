using Microsoft.AspNetCore.Identity;

namespace TurfTime.Web.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider sp)
        {
            var roleManager = sp.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = sp.GetRequiredService<UserManager<IdentityUser>>();

            string[] roles = { "Admin", "Owner", "Player" };
            foreach (var r in roles)
            {
                if (!await roleManager.RoleExistsAsync(r))
                    await roleManager.CreateAsync(new IdentityRole(r));
            }

            await CreateUser(userManager, "admin@turftime.com", "Admin@123", "Admin");
            await CreateUser(userManager, "owner@turftime.com", "Owner@123", "Owner");
            await CreateUser(userManager, "player@turftime.com", "Player@123", "Player");
        }

        private static async Task CreateUser(UserManager<IdentityUser> um, string email, string password, string role)
        {
            if (await um.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
                var result = await um.CreateAsync(user, password);
                if (result.Succeeded)
                    await um.AddToRoleAsync(user, role);
            }
        }
    }
}