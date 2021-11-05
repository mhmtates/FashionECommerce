using Business.Abstract;
using Core.Results.ComplexTypes;
using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UIAdmin.Controllers
{
    [Authorize(Roles ="admin")]
    public class VariantsController : Controller
    {
        private readonly IVariantsService variants;
        public VariantsController(IVariantsService _variants)
        {
            variants = _variants;
        }
        [Route("/Variants/{Id:int?}")]
        public async Task<IActionResult> Index(int Id)
        {
            ViewBag.Data = variants.GetAll(Id).Data;
            return View();
        }
        [Route("/Variants/{Id:int?}")]
        [HttpPost]
        public async Task<IActionResult> Index(int Id, [Bind(include: "GroupName,Name,Price")] VariantsDto data)
        {
            data.ProductsId = Id;
            var DataMessage = variants.Add(data);
            if (DataMessage.ResultStatus == ResultStatus.Success)
            {
                ViewData["Message"] = "<div class='alert alert-success'>" + DataMessage.Message + "</div>";
            }
            else
            {
                ViewData["Message"] = "<div class='alert alert-danger'>" + DataMessage.Message + "</div>";
            }
            ViewBag.Data = variants.GetAll(Id).Data;
            return View();
        }

        [Route("/Variants/Delete/{Id:int?}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var DataMessage = variants.Delete(Id);
            if (DataMessage.ResultStatus == ResultStatus.Success)
            {
                ViewData["Message"] = "<div class='alert alert-success'>" + DataMessage.Message + "</div>";
            }
            else
            {
                ViewData["Message"] = "<div class='alert alert-danger'>" + DataMessage.Message + "</div>";
            }
            return Redirect("/Product");
        }
    }
}
