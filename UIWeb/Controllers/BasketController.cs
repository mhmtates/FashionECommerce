using Business.Abstract;
using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace UIWeb.Controllers
{
    [AllowAnonymous]
    public class BasketController : Controller
    {
        private readonly ITemporaryBasketsService basket;
        public BasketController(ITemporaryBasketsService _basket)
        {
            basket = _basket;
        }

        [Route("/sepet")]
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult DeleteBasket(int Id)
        {
            basket.Delete(Id);
            return Json("");
        }

        public JsonResult JSONStokAyari(int ID,int Durum)
        {
            return Json(basket.SepetArttirEksilt(ID,(Durum == 0)? false: true));

        }

        public IActionResult GetPartialBasket()
        {
            int Cookie = Convert.ToInt32(Request.Cookies["BasketId"]);

            return PartialView("/Views/PartialView/_PartialViewBasket.cshtml",basket.GetAll(Cookie).Data);
        }

        public JsonResult AddVariantsToBasket(int Id, int[] variantIds)
        {
            string Mesaj = "";
            if (Request.Cookies["BasketId"]== null)
            {
                int Bulunan = basket.GetByIdAuto(1).Data.UretilenId + 1;
                CookieOptions cookie = new CookieOptions();
                cookie.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Append("BasketId", Bulunan.ToString(), cookie);

                AutoBasketsDto autobasket = basket.GetByIdAuto(1).Data;
                autobasket.UretilenId++;
                basket.AutoBasketUpdate(autobasket);

                Mesaj = basket.AddUpdate(Convert.ToInt32(Id), Bulunan, variantIds).Message;
            }
            else
            {
                int Cookie = Convert.ToInt32(Request.Cookies["BasketId"]);
                Mesaj = basket.AddUpdate(Convert.ToInt32(Id), Cookie, variantIds).Message;
            }

            return Json(Mesaj);
        }
    }
}
