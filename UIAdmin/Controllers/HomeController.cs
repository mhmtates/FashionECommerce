using Microsoft.AspNetCore.Mvc;

namespace UIAdmin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

       
    }
}
