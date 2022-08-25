using Microsoft.Extensions.Hosting;

namespace iTalentBootcamp_Blog.Models.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Post> Posts { get; set; }
        public int PostCount
        {
            get
            {
                return Posts.Count;
            }
            set
            {
                PostCount = value;
            }
        }
    }
}
