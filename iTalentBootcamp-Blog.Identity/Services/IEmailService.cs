namespace iTalentBootcamp_Blog.Identity.Services
{
    public interface IEmailService
    {
        Task SendResetPasswordEmailAsync(string resetLink, string email);
    }
}
