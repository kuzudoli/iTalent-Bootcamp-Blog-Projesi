using AutoMapper;
using iTalentBootcamp_Blog.Core;
using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Core.Models;
using iTalentBootcamp_Blog.Core.Repositories;
using iTalentBootcamp_Blog.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Service.Services
{
    public class AuthService : Service<User>, IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        private delegate bool VerifyPasswordHashDelegate(string password, byte[] userPasswordHash, byte[] userPasswordSalt);
        private readonly VerifyPasswordHashDelegate VerifyPasswordHash;

        public AuthService(
            IGenericRepository<User> repository,
            IUnitOfWork unitOfWork,
            IAuthRepository authRepository,
            IMapper mapper) : base(repository, unitOfWork)
        {
            _authRepository = authRepository;
            _mapper = mapper;
            VerifyPasswordHash = new VerifyPasswordHashDelegate(VerifyPasswordHashMethod);
        }

        public async Task<CustomResponseDto<UserLoginDto>> LoginAsync(string username, string password)
        {
            if (!await _authRepository.AnyAsync(u => u.UserName == username))
                return CustomResponseDto<UserLoginDto>.Fail("Account Not Found!", 404);

            var validUser = await _authRepository.GetUserByUsername(username);

            if (!VerifyPasswordHash(password, validUser.PasswordHash, validUser.PasswordSalt))
                return CustomResponseDto<UserLoginDto>.Fail("Wrong Password!", 404);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, validUser.UserName),
                new Claim(ClaimTypes.Name, validUser.Name)
            };

            var userIdentity = new ClaimsIdentity(claims,"Login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

            UserLoginDto userLoginDto = new();
            userLoginDto.UserName = validUser.UserName;
            userLoginDto.Password = password;
            userLoginDto.Principle = principal;

            return CustomResponseDto<UserLoginDto>.Success(200,userLoginDto);
        }

        public async Task<CustomResponseDto<UserRegisterDto>> RegisterAsync(User user, string password)
        {
            if (await _authRepository.AnyAsync(u => u.UserName == user.UserName))
                return CustomResponseDto<UserRegisterDto>.Fail("User Exists", 400);

            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            byte[] passwordHash, passwordSalt;

            //Şifre hashleniyor
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _authRepository.Register(user);
            await _unitOfWork.CommitAsync();

            var userCreateDto = _mapper.Map<UserRegisterDto>(user);
            userCreateDto.Password = password;

            return CustomResponseDto<UserRegisterDto>.Success(201, userCreateDto);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
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
