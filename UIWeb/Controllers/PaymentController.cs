using Business.Abstract;
using Core.Results.ComplexTypes;
using Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;


namespace UIWeb.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ITemporaryBasketsService sepet;
        private readonly ICustomersService customers;
        private readonly IOrderDetailsService detail;
        private readonly IOrdersService order;
        

        public PaymentController(ITemporaryBasketsService _sepet,ICustomersService _customers,IOrderDetailsService _detail, IOrdersService _order)
        {
            sepet = _sepet;
            customers = _customers;
            detail = _detail;
            order = _order;
        }
            
        [Route("/Odeme")]
        public IActionResult Index()
        {
            int Cookie = Convert.ToInt32(Request.Cookies["BasketId"]);
            int BulunanUyeId = int.Parse(User.FindFirst(x => x.Type == "ID").Value.ToString());
            ViewBag.Uyeler = customers.GetById(BulunanUyeId).Data;

            return View(sepet.GetAll(Cookie).Data);
        }
        [HttpPost]
        [Route("/Odeme")]
        public IActionResult Index(string OdemeYontemi)
        {
            int BulunanUyeId = int.Parse(User.FindFirst(x => x.Type == "ID").Value.ToString());
            int Cookie = Convert.ToInt32(Request.Cookies["BasketId"]);

            decimal ToplamFiyat = 0;
            foreach (var item in sepet.GetAll(Cookie).Data)
            {
                ToplamFiyat += item.Price * item.Quantity;
            }
            OrdersUpdateDto siparis = new OrdersUpdateDto();
            siparis.Id = Cookie;
            siparis.CargoNumber = "0";
            siparis.CargoPrice = 0;
            siparis.CouponPrice = 0;
            siparis.Kdv = 18;
            siparis.OrderDate = DateTime.Now;
            siparis.TotalDiscount = 0;
            siparis.TotalPrice = ToplamFiyat;
            siparis.CustomersId = BulunanUyeId;
            siparis.PaymentType = OdemeYontemi;
            if (OdemeYontemi == "Kapıda Ödeme")
            {
                siparis.OrderStatus = "Kargoya Verildi";
            }
            else if (OdemeYontemi == "Havale İle Ödeme")
            {
                siparis.OrderStatus = "Ödeme Bekleniyor";
            }
            else 
            {
                siparis.OrderStatus = "Teslim Edildi";
            }
            var Data = order.Add(siparis).ResultStatus;

            foreach (var item in sepet.GetAll(Cookie).Data)
            {
                OrderDetailsDto siparisdetay = new OrderDetailsDto();
                siparisdetay.Name = item.Name;
                siparisdetay.VariantName = item.VariantName;
                siparisdetay.Price = item.Price;
                siparisdetay.OrdersId = Cookie;
                siparisdetay.Quantity = item.Quantity;
                siparisdetay.ProductsId = item.ProductsId;
                detail.Add(siparisdetay);

                sepet.Delete(item.Id);
            }
            if (Data == ResultStatus.Success)
            {
                TempData["Mesaj"] = Cookie + " Numaralı Siparişiniz Alınmıştır.";

                var GelenCookies = Request.Cookies["BasketId"];
                CookieOptions cookie = new CookieOptions();
                cookie.Expires = DateTime.Now.AddDays(-6);
                Response.Cookies.Append("BasketId", GelenCookies, cookie);

                return Redirect("/Odeme/Basarili");
            }
            else
            {
                return Redirect("/Odeme/Basarisiz");
            }
        }

       [Route("/Odeme/Basarili")]
      public IActionResult Basarili()
      {
           return View();
      }
      [Route("/Odeme/Basarisiz")]
     public IActionResult Basarisiz()
       {
            return View();
       }
    }
}
