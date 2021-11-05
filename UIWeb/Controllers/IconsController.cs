using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UIWeb.Controllers
{
    [AllowAnonymous]
    public class IconsController : Controller
    {
        [Route("/ikonlar")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
