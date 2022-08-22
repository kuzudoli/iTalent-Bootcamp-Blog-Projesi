using System.ComponentModel.DataAnnotations;

namespace iTalentBootcamp_Blog.Models.ViewModels
{
    public class UpdateCategoryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Kategori ismi alanı boş bırakılamaz!")]
        public string Name { get; set; }
    }
}
