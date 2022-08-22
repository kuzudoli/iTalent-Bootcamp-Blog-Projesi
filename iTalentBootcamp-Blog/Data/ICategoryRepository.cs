using iTalentBootcamp_Blog.Models;
using iTalentBootcamp_Blog.Models.ViewModels;

namespace iTalentBootcamp_Blog.Data
{
    public interface ICategoryRepository
    {
        void Add(Category category);
        void Update(Category category);
        void Delete(int id);
        Category GetById(int id);
        List<Category> GetAll();
    }
}
