namespace iTalentBootcamp_Blog.Models.ViewModels
{
    public class CreateCommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int PostId { get; set; }
    }
}
