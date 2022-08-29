using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Core.Services
{
    public interface IAuthService : IService<User>
    {
        Task<CustomResponseDto<User>> LoginAsync(string username, string password);
        Task<CustomResponseDto<UserCreateDto>> RegisterAsync(User user, string password);
    }
}
