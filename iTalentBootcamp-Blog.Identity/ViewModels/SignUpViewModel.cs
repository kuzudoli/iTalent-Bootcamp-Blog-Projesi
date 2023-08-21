using System.ComponentModel.DataAnnotations;

namespace iTalentBootcamp_Blog.Identity.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email boş bırakılamaz!")]
        [EmailAddress(ErrorMessage = "Email formatı yanlış!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon Numarası boş bırakılamaz!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Şifre boş bırakılamaz!")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Şifreler aynı değil!")]
        [Required(ErrorMessage = "Şifre Tekrar boş bırakılamaz!")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Üyelik Anahatar Kodu boş bırakılamaz!")]
        public string AuthKey { get; set; }//Herkes register olamasın diye ufak bir önlem
    }
}
