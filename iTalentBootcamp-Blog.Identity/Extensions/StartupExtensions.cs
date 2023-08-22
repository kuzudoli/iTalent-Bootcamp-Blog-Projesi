using iTalentBootcamp_Blog.Identity.CustomValidations;
using iTalentBootcamp_Blog.Identity.Entity;
using iTalentBootcamp_Blog.Identity.Repository;

namespace iTalentBootcamp_Blog.Identity.Extensions
{
    public static class StartupExtensions
    {
        public static void AddIdentityWithOpt(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<AppDbContext>()
            .AddPasswordValidator<PasswordValidator>();
        }
    }
}
