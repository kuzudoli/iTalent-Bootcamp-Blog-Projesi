using FluentValidation;
using iTalentBootcamp_Blog.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Service.Validations
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(p => p.UserName).NotEmpty().NotNull().WithMessage("Kullanıcı adı boş bırakılamaz!");
            RuleFor(p => p.Password).NotEmpty().NotNull().WithMessage("Şifre boş bırakılamaz!");
        }
    }
}
