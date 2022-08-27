using AutoMapper;
using iTalentBootcamp_Blog.Data;
using iTalentBootcamp_Blog.Models;
using iTalentBootcamp_Blog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Add(CreateCommentViewModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var newComment = _mapper.Map<Comment>(request);
            _commentRepository.Add(newComment);

            return RedirectToRoute(new { controller = "Home", action = "PostDetails", id = request.PostId });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var comment = _commentRepository.GetById(id);
            if (comment != null)
                _commentRepository.Delete(id);

            return RedirectToRoute("Posts");
        }
    }
}
