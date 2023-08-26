using System.ComponentModel.DataAnnotations;

namespace iTalentBootcamp_Blog.Identity.ViewModels
{
    public class PasswordChangeViewModel
    {
        [Required(ErrorMessage = "Eski Şifre boş bırakılamaz!")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Yeni Şifre boş bırakılamaz!")]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword), ErrorMessage = "Şifreler aynı değil!")]
        [Required(ErrorMessage = "Yeni Şifre Tekrar boş bırakılamaz!")]
        public string NewPasswordConfirm { get; set; }
    }
}
