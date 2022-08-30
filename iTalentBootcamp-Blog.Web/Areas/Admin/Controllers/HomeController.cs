using iTalentBootcamp_Blog.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly PostApiService _postApiService;
        private readonly CategoryApiService _categoryApiService;

        public HomeController(CategoryApiService categoryApiService, PostApiService postApiService)
        {
            _categoryApiService = categoryApiService;
            _postApiService = postApiService;
        }

        [HttpGet]
        [Route("Admin/Dashboard", Name = "Dashboard")]
        public async Task<IActionResult> Index()
        {
            ViewBag.postList = await _postApiService.GetPostsWithCategory();
            ViewBag.categoryList = await _categoryApiService.GetAll();

            ViewBag.postCount = ViewBag.postList.Count;
            ViewBag.categoryCount = ViewBag.categoryList.Count;

            return View();
        }
    }
}
