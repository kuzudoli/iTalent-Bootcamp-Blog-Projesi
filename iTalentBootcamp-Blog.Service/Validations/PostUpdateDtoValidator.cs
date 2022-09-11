using FluentValidation;
using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Service.Validations
{
    public class PostUpdateDtoValidator : AbstractValidator<PostUpdateDto>
    {
        public PostUpdateDtoValidator()
        {

            RuleFor(p => p.Title).NotEmpty().NotNull().WithMessage("Başlık boş bırakılamaz!")
                .MinimumLength(10).WithMessage("Başlık en az 10 karakter olmalıdır!");

            //RuleFor(p => p.Title).NotEmpty().NotNull().WithMessage("Başlık boş bırakılamaz!")
            //    .MinimumLength(10).WithMessage("Başlık en az 10 karakter olmalıdır!")
            //    .Must(IsTitleUnique).WithMessage("Bu başlık daha önce kullanılmıştır!");

            RuleFor(p => p.Content).NotEmpty().NotNull().WithMessage("İçerik boş bırakılamaz!")
                .MinimumLength(100).WithMessage("İçerik en az 100 karakter olmalıdır!");

            RuleFor(p => p.CategoryId).Must(categoryId => categoryId > 0).WithMessage("Kategori boş bırakılamaz!");
        }

        //private bool IsTitleUnique(string newPostTitle)
        //{
        //    var state = _postRepository.GetAll().Any(p => p.Title.ToLower().Equals(newPostTitle.ToLower()));
        //    return !state;
        //}
    }
}
