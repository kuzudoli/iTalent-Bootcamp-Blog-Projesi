using AutoMapper;
using iTalentBootcamp_Blog.Data;
using iTalentBootcamp_Blog.Models;
using iTalentBootcamp_Blog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace iTalentBootcamp_Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public HomeController(IPostRepository postRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [Route("/",Name ="Index")]
        public IActionResult Index(int page=1)
        {
            int pageSize = 5;

            var postList = _mapper.Map<List<PostViewModel>>(_postRepository.GetByPage(page, pageSize).Item1);

            ViewBag.postList = postList.OrderByDescending(x => x.CreatedAt).ToList();

            ViewBag.pageCount = _postRepository.GetByPage(page, pageSize).Item2;
            ViewBag.currentIndex = page;
            ViewBag.currentPageName = "anasayfa";

            return View();
        }

        [Route("/About")]
        public IActionResult About()
        {
            ViewBag.currentPageName = "hakkimda";
            return View();
        }

        [Route("/Contact")]
        public IActionResult Contact()
        {
            ViewBag.currentPageName = "iletisim";
            return View();
        }

        [HttpGet]
        [Route("/Posts/{id}")]
        public IActionResult PostDetails(int id)
        {
            var post = _postRepository.GetById(id);
            ViewBag.post = _mapper.Map<PostViewModel>(post);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}