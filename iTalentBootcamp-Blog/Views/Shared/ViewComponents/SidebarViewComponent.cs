using AutoMapper;
using iTalentBootcamp_Blog.Data;
using iTalentBootcamp_Blog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.Views.Shared.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public SidebarViewComponent(ICategoryRepository categoryRepository, IMapper mapper, IPostRepository postRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = _mapper.Map<List<CategoryViewModel>>(_categoryRepository.GetAll());
            var popularPostList = _mapper.Map<List<PostViewModel>>(_postRepository.GetPopularPosts(5));
            return await Task.FromResult(View("Default", (categories,popularPostList)));
        }
    }
}