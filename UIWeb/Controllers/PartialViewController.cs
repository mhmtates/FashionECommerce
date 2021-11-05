using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace UIWeb.Controllers
{
    [AllowAnonymous]
    public class PartialViewController : Controller
    {
        private readonly ITemporaryBasketsService db;

        public PartialViewController(ITemporaryBasketsService _db)
        {
            db = _db;
        }
        public IActionResult  BasketQuantityControl()
        {
            int Cookie = Convert.ToInt32(Request.Cookies["BasketId"]);
            if (db.GetAll(Cookie).Data == null)
            {
                return PartialView("/Views/PartialView/_PartialViewBasketQuantity.cshtml", db.GetAll(Cookie).Data);
                    
            }
            else
            {
                return PartialView("/Views/PartialView/_PartialViewBasketQuantity.cshtml", db.GetAll(Cookie).Data.Count);   
            }



           
        }
    }
}
