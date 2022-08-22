using iTalentBootcamp_Blog.Models;
using iTalentBootcamp_Blog.Models.ViewModels;

namespace iTalentBootcamp_Blog.Data
{
    public interface IPostRepository
    {
        void Add(Post post);
        void Update(Post post);
        void Delete(int id);
        Post GetById(int id);
        List<Post> GetAll();
        (List<Post>,int) GetByPage(int page, int pageSize);
    }
}
