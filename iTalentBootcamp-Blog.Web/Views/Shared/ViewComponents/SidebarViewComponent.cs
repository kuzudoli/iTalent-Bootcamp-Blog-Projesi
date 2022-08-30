using iTalentBootcamp_Blog.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.Web.Views.Shared.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly CategoryApiService _categoryApiService;
        private readonly PostApiService _postApiService;

        public SidebarViewComponent(CategoryApiService categoryApiService, PostApiService postApiService)
        {
            _categoryApiService = categoryApiService;
            _postApiService = postApiService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryApiService.GetAllWithPosts();
            var popularPosts = await _postApiService.GetPopularPosts();

            return await Task.FromResult(View("Default", (categories, popularPosts)));
        }
    }
}
