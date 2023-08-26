using System.ComponentModel.DataAnnotations;

namespace iTalentBootcamp_Blog.Identity.ViewModels
{
    public class ResetPasswordViewModel
    {

        [Required(ErrorMessage = "Şifre boş bırakılamaz!")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Şifreler aynı değil!")]
        [Required(ErrorMessage = "Şifre Tekrar boş bırakılamaz!")]
        public string PasswordConfirm { get; set; }
    }
}
