
using System.ComponentModel.DataAnnotations;

namespace iTalentBootcamp_Blog.Identity.Areas.Admin.Models
{
    public class RoleCreateViewModel
    {
        [Required(ErrorMessage = "Rol adı zorunludur.")]        
        public string Name { get; set; }
    }
}
