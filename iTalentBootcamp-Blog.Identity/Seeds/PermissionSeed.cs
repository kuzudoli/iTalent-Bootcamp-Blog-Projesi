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
            if (!hasBasicRole)
            {
                await roleManager.CreateAsync(new()
                {
                    Name = "BasicRole"
                });

                var basicRole = await roleManager.FindByNameAsync("BasicRole");
                await roleManager.AddClaimAsync(basicRole, new Claim("Permission", Permissions.Stock.Read));
                await roleManager.AddClaimAsync(basicRole, new Claim("Permission", Permissions.Order.Read));
                await roleManager.AddClaimAsync(basicRole, new Claim("Permission", Permissions.Catalog.Read));
            }
        }
    }
}
