using AutoMapper;
using iTalentBootcamp_Blog.Core;
using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Core.Models;
using iTalentBootcamp_Blog.Core.Repositories;
using iTalentBootcamp_Blog.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Service.Services
{
    public class CommentService : Service<Comment>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(
            IGenericRepository<Comment> repository,
            IUnitOfWork unitOfWork,
            ICommentRepository commentRepository,
            IMapper mapper) : base(repository, unitOfWork)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task DeleteCommentById(int commentId)
        {
            await _commentRepository.DeleteCommentById(commentId);
            await _unitOfWork.CommitAsync();
        }

        public async Task<CustomResponseDto<List<CommentDto>>> GetCommentsByPostId(int postId)
        {
            var comments = await _commentRepository.GetCommentsByPostId(postId);
            var commentsDto = _mapper.Map<List<CommentDto>>(comments);

            return CustomResponseDto<List<CommentDto>>.Success(200, commentsDto);
        }
    }
}
