using iTalentBootcamp_Blog.Identity.Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace iTalentBootcamp_Blog.Identity.ClaimProviders
{
    public class UserClaimProviders : IClaimsTransformation
    {
        private readonly UserManager<AppUser> _userManager;

        public UserClaimProviders(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var userIdentity = principal.Identity as ClaimsIdentity;
            var user = await _userManager.FindByNameAsync(userIdentity!.Name);

            if (user == null || string.IsNullOrEmpty(user.City))
                return principal;

            if (!principal.HasClaim(x => x.Type == "city"))
            {
                Claim cityClaim = new Claim("city", user.City);
                userIdentity.AddClaim(cityClaim);
            }

            return principal;
        }
    }
}