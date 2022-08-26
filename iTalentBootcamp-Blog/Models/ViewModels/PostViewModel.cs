namespace iTalentBootcamp_Blog.Models.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public int LikeCount { get; set; }
        public string CreatedAtShortString
        {
            get
            {
                return CreatedAt.ToShortDateString();
            }
            set
            {
                CreatedAtShortString = value;
            }
        }
        public DateTime CreatedAt { get; set; }
        public CategoryViewModel Category { get; set; }

        public List<Comment> Comments { get; set; }
    }
}