using Business.Abstract;
using Core.Results.ComplexTypes;
using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace UIAdmin.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService db;
        public CategoriesController(ICategoriesService _db)
        {
            db = _db;
        }
        [Route("/Categories/{Id:int?}")]
        public async Task<IActionResult> Index(int? Id)
        {
            if (Id == null)
            {
                return View(db.GetAll(0).Data);
            }
            else
            {
                TempData["Category"] = db.GetById(Convert.ToInt32(Id)).Data.Name;
                return View(db.GetAll(Convert.ToInt32(Id)).Data);
            }
        }
        [Route("/Categories/Add/{id:int?}")]
        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            return View();
        }
        [Route("/Categories/Add/{id:int?}")]
        [HttpPost]
        public async Task<IActionResult> Add(int id,CategoriesDto data)
        {
            if (id != null)
            {
                data.ParentId = id;
                data.Id = 0;
            }
            else
            {
                data.ParentId = 0;
            }
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

        [Route("/Categories/Update/{Id:int}")]
        [HttpGet]// Form Load. İlk Açılış Sayfam.
        public async Task<IActionResult> Update(int Id)
        {
            return View(db.GetById(Id).Data);
        }
        [Route("/Categories/Update/{Id:int}")]
        [HttpPost] // Butona Tıkladıktan Sonra Çalışacak.
        public async Task<IActionResult> Update(int Id, CategoriesDto data)
        {
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
        [Route("/Categories/Delete/{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            db.Delete(Id);
            return RedirectToAction("Index", "Categories");
        }
    }
}
