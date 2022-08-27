using AutoMapper;
using iTalentBootcamp_Blog.Models;
using iTalentBootcamp_Blog.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace iTalentBootcamp_Blog.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;
        public PostRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Post post = _context.Posts.Find(id);
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }

        public List<Post> GetAll()
        {
            return _context.Posts.Include(p=>p.Category).ToList();
        }

        public Post GetById(int id)
        {
            var post = _context.Posts.AsNoTracking().Include(p=>p.Category).Include(p=>p.Comments).First(p => p.Id == id);
            return post;
        }

        public (List<Post>,int) GetByPage(int page, int pageSize)
        {
            var postList = _context.Posts.Include(p=>p.Category).Include(p=>p.Comments).ToList();
            var pagedList = postList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var pageCount = Convert.ToInt32(Math.Ceiling((decimal)postList.Count / pageSize));

            return (pagedList, pageCount);
        }

        public List<Post> GetPopularPosts(int count)
        {
            var popularPosts = _context.Posts.OrderByDescending(p => p.LikeCount).Take(count).ToList();
            return popularPosts;
        }

        public void Update(Post post)
        {
            _context.Posts.Update(post);
            _context.SaveChanges();
        }
    }
}