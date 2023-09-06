using iTalentBootcamp_Blog.Identity.Entity;
using iTalentBootcamp_Blog.Identity.Permission;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace iTalentBootcamp_Blog.Identity.Seeds
{
    public static class PermissionSeed
    {
        public static async Task SeedAsync(RoleManager<AppRole> roleManager)
        {
            var hasBasicRole = await roleManager.RoleExistsAsync("BasicRole");
            var hasAdvancedRole = await roleManager.RoleExistsAsync("AdvancedRole");
            var hasAdminRole = await roleManager.RoleExistsAsync("AdminRole");

            if (!hasBasicRole)
            {
                await roleManager.CreateAsync(new() { Name = "BasicRole" });

                var basicRole = await roleManager.FindByNameAsync("BasicRole");
                await AddReadPermission(basicRole, roleManager);
            }

            if (!hasAdvancedRole)
            {
                await roleManager.CreateAsync(new() { Name = "AdvancedRole" });

                var advancedRole = await roleManager.FindByNameAsync("AdvancedRole");
                await AddReadPermission(advancedRole, roleManager);
                await AddCreateAndUpdatePermission(advancedRole, roleManager);
            }

            if (!hasAdminRole)
            {
                await roleManager.CreateAsync(new() { Name = "AdminRole" });

                var adminRole = await roleManager.FindByNameAsync("AdminRole");
                await AddReadPermission(adminRole, roleManager);
                await AddCreateAndUpdatePermission(adminRole, roleManager);
                await AddDeletePermission(adminRole, roleManager);
            }
        }

        private static async Task AddReadPermission(AppRole role, RoleManager<AppRole> roleManager)
        {
            await roleManager.AddClaimAsync(role, new Claim("Permission", Permissions.Stock.Read));
            await roleManager.AddClaimAsync(role, new Claim("Permission", Permissions.Order.Read));
            await roleManager.AddClaimAsync(role, new Claim("Permission", Permissions.Catalog.Read));
        }

        private static async Task AddCreateAndUpdatePermission(AppRole role, RoleManager<AppRole> roleManager)
        {
            await roleManager.AddClaimAsync(role, new Claim("Permission", Permissions.Stock.Create));
            await roleManager.AddClaimAsync(role, new Claim("Permission", Permissions.Stock.Update));

            await roleManager.AddClaimAsync(role, new Claim("Permission", Permissions.Order.Create));
            await roleManager.AddClaimAsync(role, new Claim("Permission", Permissions.Order.Update));
            
            await roleManager.AddClaimAsync(role, new Claim("Permission", Permissions.Catalog.Create));
            await roleManager.AddClaimAsync(role, new Claim("Permission", Permissions.Catalog.Update));
        }

        private static async Task AddDeletePermission(AppRole role, RoleManager<AppRole> roleManager)
        {
            await roleManager.AddClaimAsync(role, new Claim("Permission", Permissions.Stock.Delete));
            await roleManager.AddClaimAsync(role, new Claim("Permission", Permissions.Order.Delete));
            await roleManager.AddClaimAsync(role, new Claim("Permission", Permissions.Catalog.Delete));
        }
    }
}
