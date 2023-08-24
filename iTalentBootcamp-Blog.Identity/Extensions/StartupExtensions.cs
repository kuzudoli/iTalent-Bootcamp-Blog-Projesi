using iTalentBootcamp_Blog.Identity.CustomValidations;
using iTalentBootcamp_Blog.Identity.Entity;
using iTalentBootcamp_Blog.Identity.Localization;
using iTalentBootcamp_Blog.Identity.Repository;

namespace iTalentBootcamp_Blog.Identity.Extensions
{
    public static class StartupExtensions
    {
        public static void AddIdentityWithOpt(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;

                opt.Lockout.MaxFailedAccessAttempts = int.Parse(configuration["IdentityOptions:FailedLoginMaxCount"]);
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            }).AddEntityFrameworkStores<AppDbContext>()
            .AddPasswordValidator<PasswordValidator>()
            .AddUserValidator<UserValidator>()
            .AddErrorDescriber<LocalizationIdentityErrorDescriber>();
        }
    }
}
