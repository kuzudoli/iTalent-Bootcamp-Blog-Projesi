using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Core.Repositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<List<Post>> GetPostsWithCategory();
        Task<Post> GetPostByIdWithCategoryAndComments(int id);
        Task<List<Post>> GetPopularPosts(int count);
        Task<Tuple<List<Post>, int>> GetPostsByPage(int page, int pageSize);
        Task<Post> GetPostByIdWithNoTracking(int id);
        Task<Post> GetPostByIdForUpdate(int id);
        Task<List<Post>> GetPostBySearch(string searchText);
        Task<Tuple<List<Post>, int>> GetPostByCategory(int categoryId, int page, int pageSize);
        void LikePost(int id);
    }
}
