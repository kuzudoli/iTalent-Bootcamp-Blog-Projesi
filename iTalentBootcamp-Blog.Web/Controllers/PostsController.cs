using AutoMapper;
using iTalentBootcamp_Blog.Core.Services;
using iTalentBootcamp_Blog.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly PostApiService _postApiService;

        public PostsController(PostApiService postApiService)
        {
            _postApiService = postApiService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}