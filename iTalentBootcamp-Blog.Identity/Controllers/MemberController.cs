using iTalentBootcamp_Blog.Identity.Entity;
using iTalentBootcamp_Blog.Identity.Extensions;
using iTalentBootcamp_Blog.Identity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.Identity.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public MemberController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);

            var userViewModel = new UserViewModel()
            {
                Username = currentUser.UserName,
                Email = currentUser.Email,
                PhoneNumber = currentUser.PhoneNumber
            };


            return View(userViewModel);
        }

        [HttpGet]
        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public IActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var checkOldPassword = await _userManager.CheckPasswordAsync(user, request.OldPassword);

            if (!checkOldPassword)
            {
                ModelState.AddModelIdentityError(new List<string>() { "Eski şifreniz yanlış" });
                return View();
            }
            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            if (!result.Succeeded)
            {
                ModelState.AddModelIdentityError(result.Errors.Select(x => x.Description).ToList());
                return View();
            }

            await _userManager.UpdateSecurityStampAsync(user);
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(user, request.NewPassword, true, false);

            TempData["SuccessMessage"] = "Şifreniz başarıyla değiştirilmiştir.";

            return RedirectToAction(nameof(Index));
        }
    }
}
