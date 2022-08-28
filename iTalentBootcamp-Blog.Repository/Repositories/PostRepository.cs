using iTalentBootcamp_Blog.Core.Models;
using iTalentBootcamp_Blog.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace iTalentBootcamp_Blog.Repository.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Post>> GetPostsWithCategory()
        {
            return await _context.Posts.Include(p => p.Category).ToListAsync();
        }

        public async Task<Post> GetPostByIdWithCategoryAndComments(int id)
        {
            return await _context.Posts.Include(p=>p.Category).Include(p=>p.Comments).FirstAsync(p=>p.Id==id);
        }

        public async Task<List<Post>> GetPopularPosts(int count)
        {
            var popularPosts = await _context.Posts.OrderByDescending(p => p.LikeCount).Take(count).ToListAsync();
            return popularPosts;
        }
    }
}
