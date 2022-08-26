using System.ComponentModel.DataAnnotations;

namespace iTalentBootcamp_Blog.Models.ViewModels
{
    public class UpdatePostViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık alanı boş bırakılamaz!")]
        [MinLength(15, ErrorMessage = "Başlık alanı minimum 15 karakter olmalıdır!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "İçerik alanı boş bırakılamaz!")]
        [MinLength(75, ErrorMessage = "İçerik alanı minimum 75 karakter olmalıdır!")]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
