using FluentValidation;
using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Core.Repositories;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Service.Validations
{
    public class PostCreateDtoValidator : AbstractValidator<PostCreateWithImageDto>
    {

        public PostCreateDtoValidator()
        {
            RuleFor(p => p.Title).NotEmpty().WithMessage("Başlık boş bırakılamaz!")
                .MinimumLength(10).WithMessage("Başlık en az 10 karakter olmalıdır!");

            //RuleFor(p => p.Title).NotEmpty().WithMessage("Başlık boş bırakılamaz!")
            //    .MinimumLength(10).WithMessage("Başlık en az 10 karakter olmalıdır!")
            //    .Must(IsTitleUnique).WithMessage("Bu başlık daha önce kullanılmıştır!");

            RuleFor(p => p.Content).NotEmpty().WithMessage("İçerik boş bırakılamaz!")
                .MinimumLength(100).WithMessage("İçerik en az 100 karakter olmalıdır!");

            RuleFor(p => p.ImageFile).NotEmpty().NotNull().WithMessage("Görsel boş bırakılamaz!");

            RuleFor(p => p.CategoryId).Must(categoryId => categoryId > 0).WithMessage("Kategori boş bırakılamaz!");
        }

        //private bool IsTitleUnique(string newPostTitle)
        //{
        //    var state = _postRepository.GetAll().Any(p=>p.Title.ToLower().Equals(newPostTitle.ToLower()));
        //    return !state;
        //}
        
    }
}
