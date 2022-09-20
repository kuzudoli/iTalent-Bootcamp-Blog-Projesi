using AutoMapper;
using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Core.Models;
using iTalentBootcamp_Blog.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace iTalentBootcamp_Blog.API.Controllers
{

    public class PostsController : CustomBaseController
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostsController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpGet("[action]/{searchText}")]
        public async Task<IActionResult> GetPostsBySearch(string searchText)
        {
            var posts = await _postService.GetPostBySearch(searchText);
            return CreateActionResult(posts);
        }

        [HttpPut("[action]/{postId}")]
        public async Task<IActionResult> LikePost(int postId)
        {
            await _postService.LikePost(postId);
            return CreateActionResult(CustomResponseDto<PostUpdateDto>.Success(204));
        }

        [HttpGet("[action]/{page}/{pageSize}")]
        public async Task<IActionResult> GetPostsByPage(int page, int pageSize)
        {
            var posts = await _postService.GetPostsByPage(page, pageSize);
            return CreateActionResult(posts);
        }

        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetPostsByCategory(int categoryId, [Required] int page, [Required] int pageSize)
        {
            var posts = await _postService.GetPostByCategory(categoryId, page, pageSize);
            return CreateActionResult(posts);
        }

        [HttpGet("[action]/{count}")]
        public async Task<IActionResult> GetPopularPosts(int count)
        {
            var popularPosts = await _postService.GetPopularPosts(count);
            return CreateActionResult(popularPosts);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPostsWithCategory()
        {
            var posts = await _postService.GetPostsWithCategory();

            return CreateActionResult(posts);
        }

        [HttpGet("[action]/{postId}")]
        public async Task<IActionResult> GetPostByIdWithCategoryAndComments(int postId)
        {
            var post = await _postService.GetPostByIdWithCategoryAndComments(postId);

            return CreateActionResult(post);
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var posts = await _postService.GetAllAsync();
            var postsDto = _mapper.Map<List<PostDto>>(posts.ToList());

            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
            return CreateActionResult(CustomResponseDto<List<PostDto>>.Success(200, postsDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var post = await _postService.GetByIdAsync(id);
            var postDto = _mapper.Map<PostDto>(post);

            return CreateActionResult(CustomResponseDto<PostDto>.Success(200, postDto));
        }

        [HttpGet("[action]/{postId}")]
        public async Task<IActionResult> GetPostByIdWithNoTracking(int postId)
        {
            var post = await _postService.GetPostByIdWithNoTracking(postId);

            return CreateActionResult(post);
        }

        [HttpGet("[action]/{postId}")]
        public async Task<IActionResult> GetPostByIdForUpdate(int postId)
        {
            var postToUpdate = await _postService.GetPostByIdForUpdate(postId);

            return CreateActionResult(postToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Save(PostCreateDto request)
        {
            var post = await _postService.AddAsync(_mapper.Map<Post>(request));
            var postDto = _mapper.Map<PostCreateDto>(post);

            return CreateActionResult(CustomResponseDto<PostCreateDto>.Success(201, postDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(PostUpdateDto request)
        {
            var requestFix = _mapper.Map<Post>(request);
            requestFix.CreatedAt = _postService.GetPostByIdWithNoTracking(request.Id).Result.Data.CreatedAt;//Fixing CreatedAt Date

            await _postService.UpdateAsync(requestFix);

            return CreateActionResult(CustomResponseDto<PostUpdateDto>.Success(204));//no content response
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var postDto = await _postService.GetPostByIdWithNoTracking(id);
            var post = _mapper.Map<Post>(postDto.Data);

            await _postService.RemoveAsync(post);

            return CreateActionResult(CustomResponseDto<PostDto>.Success(204));
        }
    }
}
