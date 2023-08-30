using iTalentBootcamp_Blog.Identity.Models;
using System.ComponentModel.DataAnnotations;

namespace iTalentBootcamp_Blog.Identity.ViewModels
{
    public class UserEditViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email boş bırakılamaz!")]
        [EmailAddress(ErrorMessage = "Email formatı yanlış!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon Numarası boş bırakılamaz!")]
        public string Phone { get; set; }

        public string? City { get; set; }
        public IFormFile? Picture { get; set; }
        public DateTime? BirthDay { get; set; }
        public Gender? Gender { get; set; }
    }
}
