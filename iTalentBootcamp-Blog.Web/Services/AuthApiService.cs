using iTalentBootcamp_Blog.Core.Dtos;
using System.Net.Http.Json;

namespace iTalentBootcamp_Blog.Web.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;

        public AuthApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserLoginDto> GetUserByUsername(UserLoginDto userLoginDto)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<UserLoginDto>>
                ($"Auth/GetUserByUsername/{userLoginDto.UserName}/{userLoginDto.Password}");

            return response.Data;
        }
    }
}
