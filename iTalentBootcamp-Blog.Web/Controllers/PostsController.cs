using AutoMapper;
using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Core.Services;
using iTalentBootcamp_Blog.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

namespace iTalentBootcamp_Blog.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly PostApiService _postApiService;

        public PostsController(PostApiService postApiService)
        {
            _postApiService = postApiService;
        }

        [HttpGet]
        [Route("Posts/{postId}/Like", Name = "LikePost")]
        public async Task<IActionResult> LikePost(int postId)
        {
            await _postApiService.LikePost(postId);

            return RedirectToRoute(
                 new
                 {
                     controller = "Posts",
                     action = "PostDetails",
                     postId = postId
                 });
        }

        [HttpGet]
        [Route("Posts/{postId}", Name = "PostDetails")]
        public async Task<IActionResult> PostDetails(int postId)
        {
            ViewBag.post = await _postApiService.GetPostByIdWithCategoryAndComments(postId);

            return View();
        }

        [HttpGet]
        [Route("Posts/Category/{categoryId}/Page/{page?}",Name = "PostByCategory")]
        public async Task<IActionResult> PostsByCategory(int categoryId, int page=1)
        {
            int pageSize = 3;
            var pagedPostsWithPageCount = await _postApiService.GetPostsByCategory(categoryId, page, pageSize);
            
            ViewBag.categoryId = categoryId;
            ViewBag.currentIndex = page;
            ViewBag.currentPageName = "anasayfa";

            
            return View(pagedPostsWithPageCount);
        }
    }
}