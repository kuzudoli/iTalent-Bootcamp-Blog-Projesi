using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
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
        private readonly IValidator<PostCreateWithImageDto> _createValidator;
        private readonly IValidator<PostUpdateDto> _updateValidator;
        private readonly PostApiService _postApiService;
        private readonly CommentApiService _commenttApiService;
        private readonly CategoryApiService _categoryApiService;
        private readonly IPhotoHelper _photoHelper;

        public PostsController(
            PostApiService postApiService,
            CategoryApiService categoryApiService,
            CommentApiService commenttApiService,
            IPhotoHelper photoHelper,
            IValidator<PostCreateWithImageDto> createValidator,
            IValidator<PostUpdateDto> updateValidator)
        {
            _postApiService = postApiService;
            _categoryApiService = categoryApiService;
            _commenttApiService = commenttApiService;
            _photoHelper = photoHelper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
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
            ValidationResult result = _createValidator.Validate(postCreateWithImageDto);
            if (!result.IsValid)
            {
                result.AddToModelState(ModelState);
                var categoryList = await _categoryApiService.GetAll();
                ViewBag.categoryList = new SelectList(categoryList, "Id", "Name");

                return View(postCreateWithImageDto);
            }
            //Saves photo to folder and gets filename
            postCreateWithImageDto.ImageUrl = await _photoHelper.PhotoSave(postCreateWithImageDto.ImageFile);
            await _postApiService.AddPost(postCreateWithImageDto);

            return RedirectToRoute("Posts");
        }

        [HttpGet]
        [Route("Admin/Posts/Update/{postId}")]
        public async Task<IActionResult> UpdatePost(int postId)
        {
            var categoryList = await _categoryApiService.GetAll();
            ViewBag.categoryList = new SelectList(categoryList, "Id", "Name");

            var postForUpdateDto = await _postApiService.GetPostByIdForUpdate(postId);

            ViewBag.comments = await _commenttApiService.GetCommentByPostId(postId);

            return View(postForUpdateDto);
        }

        [HttpPost]
        [Route("Admin/Posts/Update/{postId}", Name = "UpdatePost")]
        public async Task<IActionResult> UpdatePost(PostUpdateDto postUpdateDto, IFormFile photo)
        {
            ValidationResult result = _updateValidator.Validate(postUpdateDto);
            if (!result.IsValid)
            {
                result.AddToModelState(ModelState);

                var categoryList = await _categoryApiService.GetAll();
                ViewBag.categoryList = new SelectList(categoryList, "Id", "Name");
                ViewBag.comments = await _commenttApiService.GetCommentByPostId(postUpdateDto.Id);

                return View(postUpdateDto);
            }
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
