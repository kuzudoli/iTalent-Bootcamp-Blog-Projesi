using iTalentBootcamp_Blog.Models;

namespace iTalentBootcamp_Blog.Data
{
    public interface ICommentRepository
    {
        void Add(Comment comment);
        void Update(Comment comment);
        void Delete(int id);
        List<Comment> GetAll();
        Comment GetById(int id);
    }
}
