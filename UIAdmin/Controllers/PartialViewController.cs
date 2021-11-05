using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UIAdmin.Controllers
{
    [Authorize(Roles = "admin")]
    public class PartialViewController : Controller
    {
        private readonly ICategoriesService db;
        public PartialViewController(ICategoriesService _db)
        {
            db = _db;
        }
        public IActionResult AnaKategori()
        {
            return PartialView("/Views/PartialView/PartialKategori1.cshtml",db.GetAll(0).Data);
        }
        public IActionResult AltKategori(int Id)
        {
            return PartialView("/Views/PartialView/PartialKategori2.cshtml", db.GetAll(Id).Data);
        }
        public IActionResult AltAltKategori(int Id)
        {
            return PartialView("/Views/PartialView/PartialKategori3.cshtml", db.GetAll(Id).Data);
        }
    }
}
