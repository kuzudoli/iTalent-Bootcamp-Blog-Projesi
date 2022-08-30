using AutoMapper;
using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Core.Models;
using iTalentBootcamp_Blog.Core.Services;
using iTalentBootcamp_Blog.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.API.Controllers
{
    
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllWithPosts()
        {
            var categories = await _categoryService.GetAllWithPostsAsync();

            return CreateActionResult(categories);
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);

            return CreateActionResult(CustomResponseDto<List<CategoryDto>>.Success(200, categoriesDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);

            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(200, categoryDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryCreateDto request)
        {
            var category = await _categoryService.AddAsync(_mapper.Map<Category>(request));
            var categoryDto = _mapper.Map<CategoryCreateDto>(category);

            return CreateActionResult(CustomResponseDto<CategoryCreateDto>.Success(201, categoryDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryUpdateDto request)
        {
            await _categoryService.UpdateAsync(_mapper.Map<Category>(request));

            return CreateActionResult(CustomResponseDto<CategoryUpdateDto>.Success(204));//no content response
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            await _categoryService.RemoveAsync(category);

            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(204));
        }
    }
}
