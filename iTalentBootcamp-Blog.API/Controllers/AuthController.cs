using AutoMapper;
using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Core.Models;
using iTalentBootcamp_Blog.Core.Repositories;
using iTalentBootcamp_Blog.Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.API.Controllers
{

    public class AuthController : CustomBaseController
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserRegisterDto userCreateDto)
        {
            var newUser = _mapper.Map<User>(userCreateDto);
            var newUserCreateDto = await _authService.RegisterAsync(newUser, userCreateDto.Password);

            return CreateActionResult(newUserCreateDto);
        }

        [HttpPost("[action]/{username}/{password}")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            //var user = _mapper.Map<User>(userLoginDto);
            var validUser = await _authService.LoginAsync(userLoginDto.UserName, userLoginDto.Password);

            return CreateActionResult(validUser);
        }
    }
}