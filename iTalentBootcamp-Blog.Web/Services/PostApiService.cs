using iTalentBootcamp_Blog.Core.Dtos;

namespace iTalentBootcamp_Blog.Web.Services
{
    public class PostApiService
    {
        private readonly HttpClient _httpClient;

        public PostApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        public async Task<List<PostWithCategoryDto>> GetPostsWithCategory()
        {

            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<PostWithCategoryDto>>>
                ("Posts/GetPostsWithCategory");
            
            return response.Data;
        }

        public async Task<PostsWithPageCount> GetPostsByPage(int page, int pageSize)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<PostsWithPageCount>>
                ($"posts/GetPostsByPage/{page}/{pageSize}");
            
            return response.Data;
        }
    }
}
