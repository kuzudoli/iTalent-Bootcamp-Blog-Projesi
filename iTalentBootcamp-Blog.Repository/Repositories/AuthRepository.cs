using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Core.Models;
using iTalentBootcamp_Blog.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Repository.Repositories
{
    public class AuthRepository : GenericRepository<User>, IAuthRepository
    {
        public AuthRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.FirstAsync(u => u.UserName == username);
        }

        public async Task<User> Register(User user)
        {
            await _context.Users.AddAsync(user);
            return user;
        }

    }
}
