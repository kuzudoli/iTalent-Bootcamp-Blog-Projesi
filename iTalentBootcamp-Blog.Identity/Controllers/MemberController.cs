using iTalentBootcamp_Blog.Identity.Entity;
using iTalentBootcamp_Blog.Identity.Extensions;
using iTalentBootcamp_Blog.Identity.Models;
using iTalentBootcamp_Blog.Identity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;

namespace iTalentBootcamp_Blog.Identity.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFileProvider _fileProvider;

        public MemberController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IFileProvider fileProvider)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _fileProvider = fileProvider;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);

            var userViewModel = new UserViewModel()
            {
                Username = currentUser.UserName,
                Email = currentUser.Email,
                PhoneNumber = currentUser.PhoneNumber,
                PictureUrl = !string.IsNullOrEmpty(currentUser.Picture) ? currentUser.Picture : "default-user.jpg"
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

        [HttpGet]
        public async Task<IActionResult> UserEdit()
        {
            ViewBag.genders = new SelectList(Enum.GetNames(typeof(Gender)));

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
                throw new Exception("Bir hata oluştu!");

            var userViewModel = new UserEditViewModel()
            {
                Username = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                City = user.City,
                BirthDay = user.BirthDay,
                Gender = user.Gender
            };

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditViewModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
                throw new Exception("Bir hata oluştu!");

            user.UserName = request.Username;
            user.Email = request.Email;
            user.PhoneNumber = request.Phone;
            user.BirthDay = request.BirthDay;
            user.City = request.City;
            user.Gender = request.Gender;

            if (request.Picture != null && request.Picture.Length > 0)
            {
                var root = _fileProvider.GetDirectoryContents("wwwroot");
                var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(request.Picture.FileName)}";

                var newFilePath = Path.Combine(root.First(x => x.Name == "user-images").PhysicalPath, newFileName);

                using (var stream = new FileStream(newFilePath, FileMode.Create))
                    await request.Picture.CopyToAsync(stream);

                user.Picture = newFileName;
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelIdentityError(result.Errors.Select(x => x.Description).ToList());
                return View();
            }
            await _userManager.UpdateSecurityStampAsync(user);
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(user, true);

            TempData["SuccessMessage"] = "Profiliniz başarıyla güncellendi.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AccessDenied(string ReturnUrl)
        {
            string message = "Bu sayfayı görmek için gerekli yetkiniz bulunmamakta. Lütfen yöneticiniz ile görüşün.";
            ViewBag.message = message;

            return View();
        }
    }
}
