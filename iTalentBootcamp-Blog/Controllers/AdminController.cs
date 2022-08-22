using iTalentBootcamp_Blog.Data;
using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.Controllers
{
    public class AdminController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;

        public AdminController(
            IPostRepository postRepository,
            ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
        }

        [Route("Dashboard", Name = "Dashboard")]
        public IActionResult Index()
        {
            ViewBag.postList = _postRepository.GetAll();
            ViewBag.categoryList = _categoryRepository.GetAll();

            ViewBag.postCount = ViewBag.postList.Count;
            ViewBag.categoryCount = ViewBag.categoryList.Count;
            return View();
        }
    }
}