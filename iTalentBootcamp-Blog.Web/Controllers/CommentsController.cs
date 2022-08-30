using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.Web.Controllers
{
    public class CommentsController : Controller
    {
        private readonly CommentApiService _commentApiService;

        public CommentsController(CommentApiService commentApiService)
        {
            _commentApiService = commentApiService;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentCreateDto commentCreateDto)
        {
            await _commentApiService.AddComment(commentCreateDto);

            return RedirectToRoute(
                new { 
                    controller = "Posts", 
                    action = "PostDetails", 
                    postId = commentCreateDto.PostId 
                });
        }
    }
}
