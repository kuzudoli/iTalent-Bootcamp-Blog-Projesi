using iTalentBootcamp_Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace iTalentBootcamp_Blog.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if(category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public List<Category> GetAll()
        {
            return _context.Categories.Include(c => c.Posts).ToList();
        }

        public Category GetById(int id)
        {
            return _context.Categories.AsNoTracking().First(c => c.Id == id);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
    }
}
