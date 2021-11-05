using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UIWeb.Models;

namespace UIWeb.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        private readonly IProductsService product;
        private readonly ICategoriesService category;
        private readonly IProductImagesService image;
        private readonly IVariantsService variant;


        public ProductController(IProductsService _product,IProductImagesService _image,IVariantsService _variant, ICategoriesService _category)
        {
            product = _product;
            image = _image;
            variant = _variant;
            category = _category;
        }
        public IActionResult Index()
        {
            return View();
        }
         [Route("/urun/{UrunAdi}/{Id:int}")]
        public IActionResult Detail(string UrunAdi, int Id)
        {
            ViewBag.Images = image.GetAll(Id).Data;
            ViewBag.Variants = variant.GetAll(Id).Data?.GroupBy(x => x.GroupName);
            return View(product.GetById(Id).Data);
        }
        public JsonResult AutoComplete(string term) 
        {
            IList<ProductCategoryViewModel> model = new List<ProductCategoryViewModel>();
            var products = product.Search(term);
            foreach (var product in products.Data)
            {
                model.Add(new ProductCategoryViewModel { Type = 0, Product = product });
            }
            var categories = category.Search(term);
            foreach (var category in categories.Data)
            {
                model.Add(new ProductCategoryViewModel { Type = 1, Category = category });
            }
            return Json(model);
        }
    }
}
