using AutoMapper;
using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Web.Helpers;
using iTalentBootcamp_Blog.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iTalentBootcamp_Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly PostApiService _postApiService;
        private readonly CommentApiService _commenttApiService;
        private readonly CategoryApiService _categoryApiService;
        private readonly IPhotoHelper _photoHelper;
        private readonly IMapper _mapper;

        public PostsController(
            PostApiService postApiService,
            CategoryApiService categoryApiService,
            IMapper mapper,
            CommentApiService commenttApiService,
            IPhotoHelper photoHelper)
        {
            _postApiService = postApiService;
            _categoryApiService = categoryApiService;
            _mapper = mapper;
            _commenttApiService = commenttApiService;
            _photoHelper = photoHelper;
        }

        [HttpGet]
        [Route("Admin/Posts", Name = "Posts")]
        public async Task<IActionResult> Posts()
        {
            var postList = await _postApiService.GetPostsWithCategory();
            return View(postList);
        }

        [HttpGet]
        [Route("Admin/Posts/Add")]
        public async Task<IActionResult> AddPost()
        {
            var categoryList = await _categoryApiService.GetAll();
            ViewBag.categoryList = new SelectList(categoryList, "Id", "Name");

            return View();
        }

        [HttpPost]
        [Route("Admin/Posts/Add", Name = "AddPost")]
        public async Task<IActionResult> AddPost(PostCreateWithImageDto postCreateWithImageDto)
        {
            var postCreateDto = _mapper.Map<PostCreateDto>(postCreateWithImageDto);
            //Saves photo to folder and gets filename
            postCreateDto.ImageUrl = await _photoHelper.PhotoSave(postCreateWithImageDto.ImageFile);
            await _postApiService.AddPost(postCreateDto);

            return RedirectToRoute("Posts");
        }

        [HttpGet]
        [Route("Admin/Posts/Update/{postId}")]
        public async Task<IActionResult> UpdatePost(int postId)
        {
            var categoryList = await _categoryApiService.GetAll();
            ViewBag.categoryList = new SelectList(categoryList, "Id", "Name");

            var postForUpdate = await _postApiService.GetPostByIdWithNoTracking(postId);
            var postForUpdateDto = _mapper.Map<PostUpdateDto>(postForUpdate);

            ViewBag.comments = await _commenttApiService.GetCommentByPostId(postId);

            return View(postForUpdateDto);
        }

        [HttpPost]
        [Route("Admin/Posts/Update/{postId}", Name = "UpdatePost")]
        public async Task<IActionResult> UpdatePost(PostUpdateDto postUpdateDto, IFormFile photo)
        {
            var oldPostDto = await _postApiService.GetPostByIdWithNoTracking(postUpdateDto.Id);

            postUpdateDto.ImageUrl = await _photoHelper.PhotoUpdate(oldPostDto.ImageUrl, photo);
            await _postApiService.UpdatePost(postUpdateDto);

            return RedirectToRoute("Posts");
        }

        [HttpGet]
        [Route("Admin/Posts/Delete/{postId}", Name = "DeletePost")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            await _postApiService.DeletePost(postId);

            return RedirectToRoute("Posts");
        }

    }
}
