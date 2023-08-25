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
        private readonly int _failedLoginMaxCount;

        public AuthController(UserManager<AppUser> userManager, IConfiguration conf, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _conf = conf;
            _authKey = _conf["AuthKey"];
            _signInManager = signInManager;
            _failedLoginMaxCount = int.Parse(_conf["IdentityOptions:FailedLoginMaxCount"]);
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
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = Url.Action("Index", "Home");

            var hasUser = await _userManager.FindByEmailAsync(request.Email);
            if (hasUser == null)
            {
                ModelState.AddModelIdentityError(new List<string>() { "Email veya Şifre yanlış, lütfen bilgilerinizi kontrol ediniz." });
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(hasUser, request.Password, request.RememberMe, true);

            if (result.Succeeded)
                return Redirect(returnUrl);

            if (result.IsLockedOut)
            {
                ModelState.AddModelIdentityError(new List<string>() { "Çok fazla hatalı giriş yaptınız, 3 dakika sonra tekrar deneyiniz." });
                return View();
            }

            string errStr;
            var userAttemptCount = await _userManager.GetAccessFailedCountAsync(hasUser);

            if (userAttemptCount == _failedLoginMaxCount - 1)
                errStr = "Email veya Şifre yanlış, lütfen bilgilerinizi kontrol ediniz. Tekrar hatalı giriş yaparsanız hesabınız geçiçi olarak kilitlenecektir.";
            else
                errStr = "Email veya Şifre yanlış, lütfen bilgilerinizi kontrol ediniz.";

            ModelState.AddModelIdentityError(new List<string>() { errStr });
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                TempData["Info"] = "Şifre sıfırlama bağlantısı, e-posta adresine gönderildi.";
                return RedirectToAction(nameof(ForgotPassword));
            }

            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            string passwordResetLink = Url.Action("ResetPassword", "Auth", new {userId=user.Id,token=passwordResetToken})!;

            //Email Service

            TempData["Info"] = "Şifre sıfırlama bağlantısı, e-posta adresine gönderildi.";
            return RedirectToAction(nameof(ForgotPassword));
        }
    }
}
