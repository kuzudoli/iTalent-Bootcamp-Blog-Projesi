using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace iTalentBootcamp_Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly AuthApiService _authApiService;

        public AuthController(AuthApiService authApiService)
        {
            _authApiService = authApiService;
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
            var validUser = await _authApiService.Login(userLoginDto.UserName, userLoginDto.Password);

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
