using System.ComponentModel.DataAnnotations;

namespace iTalentBootcamp_Blog.Models.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required(ErrorMessage = "Kategori ismi alanı boş bırakılamaz!")]
        public string Name { get; set; }
    }
}
