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

        public async Task<List<PostDto>> GetPostsBySearch(string searchText)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<PostDto>>>
                ($"Posts/GetPostsBySearch/{searchText}");

            return response.Data;
        }

        public async Task<PostDto> GetById(int postId)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<PostDto>>
                ($"Posts/{postId}");

            return response.Data;
        }

        public async Task<PostDto> GetPostByIdWithNoTracking(int postId)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<PostDto>>
                ($"Posts/GetPostByIdWithNoTracking/{postId}");

            return response.Data;
        }

        public async Task LikePost(int postId)
        {
            await _httpClient.PutAsJsonAsync($"Posts/LikePost/{postId}", postId);
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

        public async Task<List<PostPopularDto>> GetPopularPosts()
        {
            int count = 4;
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<PostPopularDto>>>
                ($"Posts/GetPopularPosts/{count}");

            return response.Data;
        }

        public async Task<PostWithCategoryAndCommentsDto> GetPostByIdWithCategoryAndComments(int postId)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<PostWithCategoryAndCommentsDto>>
                ($"Posts/GetPostByIdWithCategoryAndComments/{postId}");

            return response.Data;
        }

        public async Task DeletePost(int postId)
        {
            await _httpClient.DeleteAsync($"Posts/{postId}");
        }

        public async Task AddPost(PostCreateDto postCreateDto)
        {
            await _httpClient.PostAsJsonAsync($"Posts", postCreateDto);
        }

        internal async Task UpdatePost(PostUpdateDto postUpdateDto)
        {
            await _httpClient.PutAsJsonAsync($"Posts", postUpdateDto);
        }
    }
}
