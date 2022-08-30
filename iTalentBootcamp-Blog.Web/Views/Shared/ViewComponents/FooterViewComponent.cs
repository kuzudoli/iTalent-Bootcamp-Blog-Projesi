using AutoMapper;
using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.Web.Views.Shared.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly CategoryApiService _categoryApiService;

        public FooterViewComponent(CategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryApiService.GetAllWithPosts();
            return await Task.FromResult(View("Default", categories));
        }
    }
}
