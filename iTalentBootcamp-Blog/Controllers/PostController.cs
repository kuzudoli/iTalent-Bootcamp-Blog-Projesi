using AutoMapper;
using iTalentBootcamp_Blog.Data;
using iTalentBootcamp_Blog.Models;
using iTalentBootcamp_Blog.Models.ViewModels;
using iTalentBootcamp_Blog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;

namespace iTalentBootcamp_Blog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IFileProvider _fileProvider;
        private readonly IPhotoService _photoService;

        public PostController(
            IPostRepository postRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper,
            IFileProvider fileProvider,
            IPhotoService photoService)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _fileProvider = fileProvider;
            _photoService = photoService;
        }

        [HttpGet]
        [Route("/Posts",Name ="Posts")]
        public IActionResult GetAll()
        {
            var posts = _postRepository.GetAll();
            List<PostViewModel> postList = _mapper.Map<List<PostViewModel>>(posts);
            return View(postList);
        }

        [HttpGet]
        [Route("/Posts/AddPost")]
        public IActionResult CreatePost()
        {
            var categoryList = _categoryRepository.GetAll();
            ViewBag.categoryList = new SelectList(categoryList, "Id", "Name");
            return View();
        }

        [HttpPost]
        [Route("/Posts/AddPost",Name ="AddPost")]
        public async Task<IActionResult> CreatePost(CreatePostViewModel request)
        {
            if (!ModelState.IsValid)
            {
                var categoryList = _categoryRepository.GetAll();
                ViewBag.categoryList = new SelectList(categoryList, "Id", "Name");

                return View(request);
            }
            var newPost = _mapper.Map<Post>(request);

            var photoFileName = await _photoService.PhotoSave(request.ImageFile);
            newPost.ImageUrl = photoFileName;

            _postRepository.Add(newPost);

            return RedirectToAction("GetAll","Post");
        }

        [HttpGet]
        [Route("/Posts/Delete/{id}")]
        public IActionResult DeletePost(int id)
        {
            _postRepository.Delete(id);
            return RedirectToAction("GetAll", "Post");
        }

        [HttpGet]
        [Route("/Posts/Update/{id}")]
        public IActionResult UpdatePost(int id)
        {
            var categoryList = _categoryRepository.GetAll();
            ViewBag.categoryList = new SelectList(categoryList, "Id", "Name");

            UpdatePostViewModel postToUpdate = _mapper.Map<UpdatePostViewModel>(_postRepository.GetById(id));

            return View(postToUpdate);
        }

        [HttpPost]
        [Route("/Posts/Update",Name ="UpdatePost")]
        public async Task<IActionResult> UpdatePost(UpdatePostViewModel request, IFormFile photo)
        {
            var imageUrl = await _photoService.PhotoUpdate(request.Id, photo);

            request.ImageUrl = imageUrl;

            var postUpdated = _mapper.Map<Post>(request);

            _postRepository.Update(postUpdated);

            return RedirectToAction("GetAll", "Post");
        }

        [HttpGet]
        [Route("/Posts/Detail/{id}")]
        public IActionResult GetPost(int id)
        {
            var postById = _postRepository.GetById(id);
            var postViewModel = _mapper.Map<PostViewModel>(postById);

            return View(postViewModel);
        }
    }
}
