using AutoMapper;
using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iTalentBootcamp_Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly CategoryApiService _categoryApiService;
        private readonly IMapper _mapper;
        public CategoriesController(CategoryApiService categoryApiService, IMapper mapper)
        {
            _categoryApiService = categoryApiService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Admin/Categories", Name = "Categories")]
        public async Task<IActionResult> Categories()
        {
            var categoryList = await _categoryApiService.GetAll();
            return View(categoryList);
        }

        [HttpGet]
        [Route("Admin/Categories/Add")]
        public async Task<IActionResult> AddCategory()
        {
            return View();
        }

        [HttpPost]
        [Route("Admin/Categories/Add", Name = "AddCategory")]
        public async Task<IActionResult> AddCategory(CategoryCreateDto categoryCreateDto)
        {
            await _categoryApiService.AddCategory(categoryCreateDto);
            return RedirectToRoute("Categories");
        }

        [HttpGet]
        [Route("Admin/Categories/Update/{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId)
        {
            var categoryForUpdate = await _categoryApiService.GetById(categoryId);
            var categoryForUpdateDto = _mapper.Map<CategoryUpdateDto>(categoryForUpdate);

            return View(categoryForUpdateDto);
        }

        [HttpPost]
        [Route("Admin/Categories/Update/{categoryId}", Name = "UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateDto categoryUpdateDto)
        {
            await _categoryApiService.UpdateCategory(categoryUpdateDto);

            return RedirectToRoute("Categories");
        }

        [HttpGet]
        [Route("Admin/Categories/Delete/{categoryId}", Name = "DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            await _categoryApiService.DeleteCategory(categoryId);

            return RedirectToRoute("Categories");
        }
    }
}
