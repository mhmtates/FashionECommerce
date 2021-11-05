using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
namespace UIWeb.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ICategoriesService categories;
       public HeaderViewComponent(ICategoriesService _categories)
        {
            categories = _categories;
        }

        public IViewComponentResult Invoke()
        {
            return View(categories.GetAll(0).Data);
        }
    }
}
