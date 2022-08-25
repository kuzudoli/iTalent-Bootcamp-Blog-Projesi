using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.Views.Admin.ViewComponents
{
    public class WelcomeTextViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var time = DateTime.Now.TimeOfDay;
            var userName = "(Name)";
            var dayText = " İyi Günler 🌞";

            if (time > new TimeSpan(16, 00, 00) && time < new TimeSpan(00, 00, 00))
            {
                dayText = " İyi Akşamlar 🌓";
            }else if(time > new TimeSpan(00, 00, 00) && time < new TimeSpan(08,00,00))
            {
                dayText = " İyi Geceler 🌚";
            }

            var welcometext = $"Merhaba {userName}, {dayText}";
            return await Task.FromResult(View("Default", welcometext));
        }
    }
}
