using iTalentBootcamp_Blog.Identity.Areas.Admin.Models;
using iTalentBootcamp_Blog.Identity.Entity;
using iTalentBootcamp_Blog.Identity.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

        [Authorize(Roles = "admin,role-action")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin,role-action")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin,role-action")]
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

        [HttpGet]
        public async Task<IActionResult> AssignRoleToUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return View();
            ViewBag.userId = user.Id;

            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles.ToListAsync();
            var roleViewModelList = new List<AssignRoleToUserViewModel>();

            foreach (var role in roles)
            {
                roleViewModelList.Add(new AssignRoleToUserViewModel()
                {
                    Id = role.Id,
                    Name = role.Name,
                    Exist = userRoles.Contains(role.Name)
                });
            }

            return View(roleViewModelList);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(string userId, List<AssignRoleToUserViewModel> requestList)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return View();

            foreach (var role in requestList)
            {
                if (role.Exist)
                    await _userManager.AddToRoleAsync(user, role.Name);
                else
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
            }

            return View();
        }

    }
}