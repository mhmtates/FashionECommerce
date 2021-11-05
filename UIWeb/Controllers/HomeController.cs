using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace UIWeb.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IProductsService product;
        private readonly ISlidesService slide;
        private readonly IVariantsService variant;
        private readonly ITemporaryBasketsService basket;
        private readonly ICategoriesService category;
        public HomeController(IProductsService _product, ISlidesService _slide, IVariantsService _variant, ITemporaryBasketsService _basket, ICategoriesService _category)
        {
            product = _product;
            slide = _slide;
            variant = _variant;
            basket = _basket;
            category = _category;
        }

        public IActionResult Index()
        {
            ViewBag.Products = product.GetAll().Data;
            ViewBag.Slides = slide.GetAll().Data;
            var categories = category.GetAll(0).Data;
            ViewBag.Categories = categories;
            return View();
        }
    }
}
