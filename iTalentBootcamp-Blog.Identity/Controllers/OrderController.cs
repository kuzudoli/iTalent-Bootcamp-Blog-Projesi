using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.Identity.Controllers
{
    public class OrderController : Controller
    {
        [Authorize(Policy = "OrderPermissionReadAndDelete")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
