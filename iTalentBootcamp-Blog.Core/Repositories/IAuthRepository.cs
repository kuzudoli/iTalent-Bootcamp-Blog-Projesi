using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Core.Repositories
{
    public interface IAuthRepository : IGenericRepository<User>
    {
        Task<User> GetUserByUsername(string username);
        Task<User> Register(User user);
    }
}
