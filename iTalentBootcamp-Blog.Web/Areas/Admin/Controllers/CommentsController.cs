using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.Web.Areas.Admin.Controllers
{
    public class CommentsController : BaseController
    {
        private readonly CommentApiService _commentApiService;

        public CommentsController(CommentApiService commentApiService)
        {
            _commentApiService = commentApiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Posts/Comments/Delete/{commentId}", Name = "DeleteComment")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            await _commentApiService.DeleteComment(commentId);

            return RedirectToRoute("Posts");
        }
    }
}
