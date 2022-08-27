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

        public async Task<List<Post>> GetPostWithCategoryAndComments()
        {
            return await _context.Posts.Include(p=>p.Category).Include(p=>p.Comments).ToListAsync();
        }
    }
}
