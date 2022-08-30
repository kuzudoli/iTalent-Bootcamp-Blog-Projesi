using iTalentBootcamp_Blog.Core.Dtos;
using System.Runtime.InteropServices;

namespace iTalentBootcamp_Blog.Web.Services
{
    public class CommentApiService
    {
        private readonly HttpClient _httpClient;

        public CommentApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CommentDto>> GetCommentByPostId(int postId)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<CommentDto>>>
                ($"Comments/{postId}");

            return response.Data;
        }

        public async Task AddComment(CommentCreateDto request)
        {
            await _httpClient.PostAsJsonAsync("Comments", request);
        }

        public async Task DeleteComment(int commentId)
        {
            await _httpClient.DeleteAsync($"Comments/{commentId}");
        }
    }
}
