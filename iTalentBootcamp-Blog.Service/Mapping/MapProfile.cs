using AutoMapper;
using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Post, PostCreateDto>().ReverseMap();
            CreateMap<Post, PostUpdateDto>().ReverseMap();
            CreateMap<Post, PostWithCategoryDto>();
            CreateMap<Post, PostWithCategoryAndCommentsDto>();
            CreateMap<Post, PostPopularDto>();
            CreateMap<Tuple<List<Post>, int>, PostsWithPageCount>()
                .ForMember(p=>p.Posts, t=> t.MapFrom(s=>s.Item1))
                .ForMember(p=>p.pageCount, t=>t.MapFrom(s=>s.Item2));


            CreateMap<Category, CategoryDto>().ReverseMap();


            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<CommentCreateDto, Comment>();

        }
    }
}
