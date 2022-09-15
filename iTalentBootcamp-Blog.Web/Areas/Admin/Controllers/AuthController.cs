using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace iTalentBootcamp_Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly AuthApiService _authApiService;
        private readonly IValidator<UserLoginDto> _loginValidator;
        public AuthController(AuthApiService authApiService, IValidator<UserLoginDto> loginValidator)
        {
            _authApiService = authApiService;
            _loginValidator = loginValidator;
        }

        [HttpGet]
        [Route("[area]/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("[area]/Login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            ValidationResult result = _loginValidator.Validate(userLoginDto);
            if (!result.IsValid)
            {
                result.AddToModelState(ModelState);
                return View(userLoginDto);
            }

            var validUser = await _authApiService.GetUserByUsername(userLoginDto);

            if (validUser == null)
                return View(userLoginDto);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, validUser.UserName)
            };

            var userIdentity = new ClaimsIdentity(claims, "Login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

            await HttpContext.SignInAsync(principal);
            return RedirectToRoute("Dashboard");
        }
    }
}
