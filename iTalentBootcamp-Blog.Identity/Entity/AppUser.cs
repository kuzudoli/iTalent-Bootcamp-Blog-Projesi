using iTalentBootcamp_Blog.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace iTalentBootcamp_Blog.Identity.Entity
{
    public class AppUser : IdentityUser
    {
        public string? City { get; set; }
        public string? Picture { get; set; }
        public DateTime? BirthDay { get; set; }
        public Gender? Gender { get; set; }
    }
}
