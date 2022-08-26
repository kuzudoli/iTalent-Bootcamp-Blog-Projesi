using AutoMapper;
using iTalentBootcamp_Blog.Models;
using iTalentBootcamp_Blog.Models.ViewModels;

namespace iTalentBootcamp_Blog.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PostViewModel, Post>().ReverseMap();
            CreateMap<CreatePostViewModel, Post>().ReverseMap();
            CreateMap<UpdatePostViewModel, Post>().ReverseMap();
            
            CreateMap<CategoryViewModel, Category>().ReverseMap();
            CreateMap<CreateCategoryViewModel, Category>().ReverseMap();
            CreateMap<UpdateCategoryViewModel, Category>().ReverseMap();

            CreateMap<CreateCommentViewModel, Comment>().ReverseMap();
        }
    }
}
