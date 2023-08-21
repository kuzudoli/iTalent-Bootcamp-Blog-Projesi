using iTalentBootcamp_Blog.Identity.Entity;
using iTalentBootcamp_Blog.Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.Identity.Controllers
{
    public class AuthController : Controller
    {
        private readonly string _authKey;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _conf;

        public AuthController(UserManager<AppUser> userManager, IConfiguration conf)
        {
            _userManager = userManager;
            _conf = conf;
            _authKey = _conf["AuthKey"];
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {
            if (!ModelState.IsValid)
                return View();

            if (!string.Equals(request.AuthKey, _authKey))
            {
                ModelState.AddModelError(String.Empty, "Girilen üyelik anahtar kodu geçersiz!");
                return View();
            }

            var identityResult = await _userManager.CreateAsync(new AppUser
            {
                UserName = request.Username,
                Email = request.Email,
                PhoneNumber = request.Phone
            }, request.Password);

            if (identityResult.Succeeded)
            {
                ViewBag.Message = "Üyelik kayıt işlemi başarıyla gerçekleştirildi.";
                return View("SignIn");
            }

            foreach (var err in identityResult.Errors)
                ModelState.AddModelError(string.Empty, err.Description);

            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }
    }
}
