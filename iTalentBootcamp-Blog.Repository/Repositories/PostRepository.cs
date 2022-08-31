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
            return await _context.Posts.Include(p => p.Category).Include(p => p.Comments).FirstAsync(p => p.Id == id);
        }

        public async Task<List<Post>> GetPopularPosts(int count)
        {
            var popularPosts = await _context.Posts
                .OrderByDescending(p => p.LikeCount)
                .Include(p => p.Comments)
                .Take(count).ToListAsync();

            return popularPosts;
        }

        public async Task<Tuple<List<Post>, int>> GetPostsByPage(int page, int pageSize)
        {
            var posts = await _context.Posts.Include(p => p.Category).Include(p => p.Comments).ToListAsync();
            var pagedPostList = posts.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var pageCount = Convert.ToInt32(Math.Ceiling((decimal)posts.Count / pageSize));

            var data = new Tuple<List<Post>, int>(pagedPostList, pageCount);
            return (data);
        }

        public void LikePost(int id)
        {
            var likedPost = _context.Posts.Find(id);
            likedPost.LikeCount++;

            Update(likedPost);
        }

        public async Task<Post> GetPostByIdWithNoTracking(int id)
        {
            return await _context.Posts.Include(p=>p.Category).AsNoTracking().FirstAsync(p => p.Id == id);
        }
    }
}
