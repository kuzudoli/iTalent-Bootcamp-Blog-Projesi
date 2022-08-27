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
    public class PostService : Service<Post>, IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostService(
            IGenericRepository<Post> repository, 
            IUnitOfWork unitOfWork, 
            IPostRepository postRepository, 
            IMapper mapper) : base(repository, unitOfWork)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<PostWithCategoryDto>>> GetPostsWithCategory()
        {
            var posts = await _postRepository.GetPostsWithCategory();
            var postsDto = _mapper.Map<List<PostWithCategoryDto>>(posts);

            return CustomResponseDto<List<PostWithCategoryDto>>.Success(200, postsDto);
        }

        public async Task<CustomResponseDto<List<PostWithCategoryAndCommentsDto>>> GetPostWithCategoryAndComments()
        {
            var posts = await _postRepository.GetPostWithCategoryAndComments();
            var postsDto = _mapper.Map<List<PostWithCategoryAndCommentsDto>>(posts);

            return CustomResponseDto<List<PostWithCategoryAndCommentsDto>>.Success(200, postsDto);
        }
    }
}
