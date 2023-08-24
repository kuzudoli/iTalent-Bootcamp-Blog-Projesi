using iTalentBootcamp_Blog.Identity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.Identity.Controllers
{
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public MemberController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
