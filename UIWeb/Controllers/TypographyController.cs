using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UIWeb.Controllers
{
     [AllowAnonymous]
    public class TypographyController : Controller
    {
        [Route("/tipografi")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
