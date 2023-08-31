using iTalentBootcamp_Blog.Identity.Areas.Admin.Models;
using iTalentBootcamp_Blog.Identity.Entity;
using iTalentBootcamp_Blog.Identity.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.Identity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RolesController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateViewModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _roleManager.CreateAsync(new AppRole { Name = request.Name });
            if (!result.Succeeded)
            {
                ModelState.AddModelIdentityError(result.Errors.Select(x=>x.Description).ToList());
                return View();
            }

            return RedirectToAction(nameof(RolesController.Index));
        }
    }
}