using System.ComponentModel.DataAnnotations;

namespace iTalentBootcamp_Blog.Identity.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email boş bırakılamaz!")]
        [EmailAddress(ErrorMessage = "Email formatı yanlış!")]
        public string Email { get; set; }
    }
}
