using AutoMapper;
using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Core.Models;
using iTalentBootcamp_Blog.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPostsWithCategory()
        {
            var posts = await _postService.GetPostsWithCategory();

            return CreateActionResult(posts);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPostWithCategoryAndComments()
        {
            var posts = await _postService.GetPostWithCategoryAndComments();
            
            return CreateActionResult(posts);
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
            await _postService.UpdateAsync(_mapper.Map<Post>(request));

            return CreateActionResult(CustomResponseDto<PostUpdateDto>.Success(204));//no content response
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _postService.GetByIdAsync(id);
            await _postService.RemoveAsync(post);

            return CreateActionResult(CustomResponseDto<PostDto>.Success(204));
        }
    }
}
