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
        public IActionResult AddComment(CreateCommentViewModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var newComment = _mapper.Map<Comment>(request);
            _commentRepository.Add(newComment);

            return RedirectToRoute("Index");
        }
    }
}
