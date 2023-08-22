using System.ComponentModel.DataAnnotations;

namespace iTalentBootcamp_Blog.Identity.ViewModels
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Email boş bırakılamaz!")]
        [EmailAddress(ErrorMessage = "Email formatı yanlış!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre boş bırakılamaz!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
