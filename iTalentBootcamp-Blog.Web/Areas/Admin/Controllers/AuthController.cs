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

        [HttpGet]
        [Route("[area]/Login/{username}/{password}")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var validUser = await _authApiService.Login(username, password);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, validUser.UserName)
            };

            var userIdentity = new ClaimsIdentity(claims, "Login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

            await HttpContext.SignInAsync(principal);
            return Ok();
        }
    }
}
