using iTalentBootcamp_Blog.Core.Dtos;
using System.Net.Http.Json;

namespace iTalentBootcamp_Blog.Web.Services
{
    public class CategoryApiService
    {
        private readonly HttpClient _httpClient;

        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryWithPostsDto>> GetAllWithPosts()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<CategoryWithPostsDto>>>
                ("Categories/GetAllWithPosts");

            return response.Data;
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<CategoryDto>>>
                ("Categories");

            return response.Data;
        }

        internal async Task<CategoryDto> GetById(int categoryId)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<CategoryDto>>
                ($"Categories/{categoryId}");

            return response.Data;
        }

        public async Task DeleteCategory(int categoryId)
        {
            await _httpClient.DeleteAsync($"Categories/{categoryId}");
        }

        public async Task UpdateCategory(CategoryUpdateDto categoryUpdateDto)
        {
            await _httpClient.PutAsJsonAsync($"Categories", categoryUpdateDto);
        }

        public async Task AddCategory(CategoryCreateDto categoryCreateDto)
        {
            await _httpClient.PostAsJsonAsync($"Categories", categoryCreateDto);
        }
    }
}
