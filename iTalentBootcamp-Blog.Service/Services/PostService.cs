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

        public async Task<CustomResponseDto<PostWithCategoryAndCommentsDto>> GetPostByIdWithCategoryAndComments(int postId)
        {
            var post = await _postRepository.GetPostByIdWithCategoryAndComments(postId);
            var postDto = _mapper.Map<PostWithCategoryAndCommentsDto>(post);

            return CustomResponseDto<PostWithCategoryAndCommentsDto>.Success(200, postDto);
        }

        public async Task<CustomResponseDto<List<PostPopularDto>>> GetPopularPosts(int count)
        {
            var posts = await _postRepository.GetPopularPosts(count);
            var postsDto = _mapper.Map<List<PostPopularDto>>(posts);

            return CustomResponseDto<List<PostPopularDto>>.Success(200, postsDto);
        }

        public async Task<CustomResponseDto<PostsWithPageCount>> GetPostsByPage(int page, int pageSize)
        {
            var posts = await _postRepository.GetPostsByPage(page, pageSize);

            var postsDto = _mapper.Map<PostsWithPageCount>(posts);

            return CustomResponseDto<PostsWithPageCount>.Success(200, postsDto);
        }

        public async Task LikePost(int id)
        {
            _postRepository.LikePost(id);
            await _unitOfWork.CommitAsync();//_unitofwork service içerisinden protected olarak verildi
        }

        public async Task<CustomResponseDto<PostDto>> GetPostByIdWithNoTracking(int id)
        {
            var post = await _postRepository.GetPostByIdWithNoTracking(id);
            var postDto = _mapper.Map<PostDto>(post);

            return CustomResponseDto<PostDto>.Success(200, postDto);
        }

        public async Task<CustomResponseDto<List<PostSearchResultDto>>> GetPostBySearch(string searchText)
        {
            var postList = await _postRepository.GetPostBySearch(searchText);
            var postListDto = _mapper.Map<List<PostSearchResultDto>>(postList);

            return CustomResponseDto<List<PostSearchResultDto>>.Success(200, postListDto);
        }
    }
}
