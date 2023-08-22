using iTalentBootcamp_Blog.Identity.Entity;
using iTalentBootcamp_Blog.Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using iTalentBootcamp_Blog.Identity.Extensions;

namespace iTalentBootcamp_Blog.Identity.Controllers
{
    public class AuthController : Controller
    {
        private readonly string _authKey;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _conf;

        public AuthController(UserManager<AppUser> userManager, IConfiguration conf, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _conf = conf;
            _authKey = _conf["AuthKey"];
            _signInManager = signInManager;
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
                ModelState.AddModelIdentityError(new List<string>() { "Girilen üyelik anahtar kodu geçersiz!" });
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

            ModelState.AddModelIdentityError(identityResult.Errors.Select(x => x.Description).ToList());

            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel request, string returnUrl)
        {
            if(string.IsNullOrEmpty(returnUrl))
                returnUrl = Url.Action("Index", "Home");

            var hasUser = await _userManager.FindByEmailAsync(request.Email);
            if (hasUser == null)
            {
                ModelState.AddModelIdentityError(new List<string>() { "Email veya Şifre yanlış, lütfen bilgilerinizi kontrol ediniz." });
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(hasUser, request.Password, request.RememberMe, false);

            if (result.Succeeded)
                return Redirect(returnUrl);

            ModelState.AddModelIdentityError(new List<string>() { "Email veya Şifre yanlış, lütfen bilgilerinizi kontrol ediniz." });

            return View();
        }
    }
}
