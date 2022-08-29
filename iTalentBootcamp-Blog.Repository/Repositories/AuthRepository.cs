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
        private delegate bool VerifyPasswordHashDelegate(string password, byte[] userPasswordHash, byte[] userPasswordSalt);
        private readonly VerifyPasswordHashDelegate VerifyPasswordHash;

        public AuthRepository(AppDbContext context) : base(context)
        {
            VerifyPasswordHash = new VerifyPasswordHashDelegate(VerifyPasswordHashMethod);
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstAsync(u => u.UserName == username);

            if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public async Task<User> Register(User user)
        {
            await _context.Users.AddAsync(user);
            return user;
        }


        private bool VerifyPasswordHashMethod(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            using (var hmac = new HMACSHA512(userPasswordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != userPasswordHash[i])
                        return false;
                }
                return true;
            }
        }
    }
}
