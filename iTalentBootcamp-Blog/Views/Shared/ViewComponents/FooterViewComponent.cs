using AutoMapper;
using iTalentBootcamp_Blog.Data;
using iTalentBootcamp_Blog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.Views.Shared.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public FooterViewComponent(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = _mapper.Map<List<CategoryViewModel>>(_categoryRepository.GetAll());
            return await Task.FromResult(View("Default", categories));
        }
    }
}
