using AutoMapper;
using iTalentBootcamp_Blog.Core;
using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Core.Models;
using iTalentBootcamp_Blog.Core.Repositories;
using iTalentBootcamp_Blog.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Service.Services
{
    public class AuthService : Service<User>, IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public AuthService(
            IGenericRepository<User> repository,
            IUnitOfWork unitOfWork,
            IAuthRepository authRepository,
            IMapper mapper) : base(repository, unitOfWork)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public Task<CustomResponseDto<User>> LoginAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomResponseDto<UserCreateDto>> RegisterAsync(User user, string password)
        {
            if (await _authRepository.AnyAsync(u=>u.UserName == user.UserName))
                return CustomResponseDto<UserCreateDto>.Fail("User Exists", 400);

            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);
            
            byte[] passwordHash, passwordSalt;
            
            //Şifre hashleniyor
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _authRepository.Register(user);
            await _unitOfWork.CommitAsync();

            var userCreateDto = _mapper.Map<UserCreateDto>(user);
            userCreateDto.Password = password;

            return CustomResponseDto<UserCreateDto>.Success(201, userCreateDto);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
