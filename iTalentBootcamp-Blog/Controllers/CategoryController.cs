using AutoMapper;
using iTalentBootcamp_Blog.Data;
using iTalentBootcamp_Blog.Models;
using iTalentBootcamp_Blog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/Categories", Name = "Categories")]
        public IActionResult GetAll()
        {
            var categories = _categoryRepository.GetAll();
            List<CategoryViewModel> categoryList = _mapper.Map<List<CategoryViewModel>>(categories);
            return View(categoryList);
        }

        [HttpGet]
        [Route("/Categories/AddCategory")]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [Route("/Categories/AddCategory", Name = "AddCategory")]
        public IActionResult CreateCategory(CreateCategoryViewModel request)
        {
            if (_categoryRepository.GetAll().Any(c => c.Name.ToLower().Equals(request.Name.ToLower())))
            {
                return View(request);
            }

            var newCategory = _mapper.Map<Category>(request);
            _categoryRepository.Add(newCategory);

            return RedirectToAction("GetAll", "Category");
        }

        [HttpGet]
        [Route("/Categories/Delete/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            _categoryRepository.Delete(id);
            return RedirectToAction("GetAll", "Category");
        }

        [HttpGet]
        [Route("/Categories/Update/{id}")]
        public IActionResult UpdateCategory(int id)
        {
            var category = _categoryRepository.GetById(id);
            UpdateCategoryViewModel postToCategory = _mapper.Map<UpdateCategoryViewModel>(category);
            return View(postToCategory);
        }

        [HttpPost]
        [Route("/Categories/Update", Name = "UpdateCategory")]
        public IActionResult UpdateCategory(UpdateCategoryViewModel request)
        {
            var categoryUpdated = _mapper.Map<Category>(request);
            _categoryRepository.Update(categoryUpdated);

            return RedirectToAction("GetAll", "Category");
        }
    }
}
