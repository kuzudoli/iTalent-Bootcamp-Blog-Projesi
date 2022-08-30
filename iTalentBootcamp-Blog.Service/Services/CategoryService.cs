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
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(
            IGenericRepository<Category> repository,
            IUnitOfWork unitOfWork,
            ICategoryRepository categoryRepository,
            IMapper mapper) : base(repository, unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<CategoryWithPostsDto>>> GetAllWithPostsAsync()
        {
            var categories = await _categoryRepository.GetAllWithPostsAsync();
            var categoriesDto = _mapper.Map<List<CategoryWithPostsDto>>(categories);

            return CustomResponseDto<List<CategoryWithPostsDto>>.Success(200,categoriesDto);
        }
    }
}
