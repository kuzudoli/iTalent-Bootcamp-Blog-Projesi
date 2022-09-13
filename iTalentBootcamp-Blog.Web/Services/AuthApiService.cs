using iTalentBootcamp_Blog.Core.Dtos;

namespace iTalentBootcamp_Blog.Web.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;

        public AuthApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserLoginDto> Login(string username, string password)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<UserLoginDto>>
                ($"Auth/Login/{username}/{password}");

            return response.Data;
        }
    }
}
