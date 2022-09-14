using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class BaseController : Controller
    {

    }
}
