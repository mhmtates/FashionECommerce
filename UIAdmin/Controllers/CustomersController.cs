using Business.Abstract;
using Core.Results.ComplexTypes;
using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UIAdmin.Controllers
{
    [Authorize(Roles = "admin")]
    public class CustomersController : Controller
    {
        private readonly ICustomersService db;
        public CustomersController(ICustomersService _db)
        {
            db = _db;
        }
        public async Task<IActionResult> Index()
        {
            return View(db.GetAll().Data);
        }
        [Route("/Customers/Add")]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [Route("/Customers/Add")]
        [HttpPost]
        public async Task<IActionResult> Add(CustomersUpdateDto data)
        {
            var DataMessage = db.Add(data);
            if (DataMessage.ResultStatus == ResultStatus.Success)
            {
                ViewData["Message"] = "<div class='alert alert-success'>" + DataMessage.Message + "</div>";
            }
            else
            {
                ViewData["Message"] = "<div class='alert alert-danger'>" + DataMessage.Message + "</div>";
            }
            return View();
        }
        [Route("/Customers/Update/{Id:int}")]
        [HttpGet]// Form Load. İlk Açılış Sayfam.
        public async Task<IActionResult> Update(int Id)
        {
            return View(db.GetById(Id).Data);
        }
        [Route("/Customers/Update/{Id:int}")]
        [HttpPost] // Butona Tıkladıktan Sonra Çalışacak.
        public async Task<IActionResult> Update(int Id, CustomersUpdateDto data)
        {
            data.Id = Id;
            var DataMessage = db.Update(data);
            if (DataMessage.ResultStatus == ResultStatus.Success)
            {
                ViewData["Message"] = "<div class='alert alert-success'>" + DataMessage.Message + "</div>";
            }
            else
            {
                ViewData["Message"] = "<div class='alert alert-danger'>" + DataMessage.Message + "</div>";
            }
            return View(db.GetById(Id).Data);// Sayfa Post Edildiğinde Güncel olan Veriyi Getiriyoruz.
        }
        [Route("/Customers/Delete/{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            db.Delete(Id);
            return RedirectToAction("Index", "Customer");
        }
    }
}
