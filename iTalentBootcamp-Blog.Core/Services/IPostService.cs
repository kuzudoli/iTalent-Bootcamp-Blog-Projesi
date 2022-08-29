using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Core.Services
{
    public interface IPostService : IService<Post>
    {
        Task<CustomResponseDto<List<PostWithCategoryDto>>> GetPostsWithCategory();
        Task<CustomResponseDto<PostWithCategoryAndCommentsDto>> GetPostByIdWithCategoryAndComments(int id);
        Task<CustomResponseDto<List<PostPopularDto>>> GetPopularPosts(int count);
        Task<CustomResponseDto<PostsWithPageCount>> GetPostsByPage(int page, int pageSize);
        Task LikePost(int id);
    }
}
