using iTalentBootcamp_Blog.Web.Models;
using iTalentBootcamp_Blog.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace iTalentBootcamp_Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PostApiService _postApiService;

        public HomeController(ILogger<HomeController> logger, PostApiService postApiService)
        {
            _logger = logger;
            _postApiService = postApiService;
        }

        [HttpGet("[action]/{page}")]
        public async Task<IActionResult> Index(int page)
        {
            //next-previous için currentIndex dönülür
            int pageSize = 3;
            var pagedPostList = await _postApiService.GetPostsByPage(page, pageSize);

            return View(pagedPostList);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}