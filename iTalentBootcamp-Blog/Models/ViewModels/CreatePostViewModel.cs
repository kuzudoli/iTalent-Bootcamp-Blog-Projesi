using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTalentBootcamp_Blog.Models.ViewModels
{
    public class CreatePostViewModel
    {
        [Required(ErrorMessage ="Başlık alanı boş bırakılamaz!")]
        [MinLength(15,ErrorMessage ="Başlık alanı minimum 15 karakter olmalıdır!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "İçerik alanı boş bırakılamaz!")]
        [MinLength(100, ErrorMessage = "İçerik alanı minimum 100 karakter olmalıdır!")]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Görsel boş bırakılamaz!")]
        public IFormFile ImageFile { get; set; }

        public int CategoryId { get; set; }
    }
}
