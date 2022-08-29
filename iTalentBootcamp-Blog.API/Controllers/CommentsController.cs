using AutoMapper;
using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Core.Models;
using iTalentBootcamp_Blog.Core.Services;
using iTalentBootcamp_Blog.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.API.Controllers
{

    public class CommentsController : CustomBaseController
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentsController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Save(CommentCreateDto request)
        {
            await _commentService.AddAsync(_mapper.Map<Comment>(request));

            return CreateActionResult(CustomResponseDto<CommentCreateDto>.Success(204));
        }
    }
}
