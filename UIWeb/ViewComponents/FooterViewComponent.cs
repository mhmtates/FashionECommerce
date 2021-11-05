using Microsoft.AspNetCore.Mvc;

namespace UIWeb.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
