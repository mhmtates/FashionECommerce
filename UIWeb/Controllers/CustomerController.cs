using Business.Abstract;
using Core.Results.ComplexTypes;
using Entities.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UIWeb.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomersService customers;
        private readonly IOrdersService orders;
        private readonly IOrderDetailsService orderdetails;
        private readonly IOrderInformationsService orderInformations;
      

        public CustomerController(ICustomersService _customers,IOrdersService _orders,IOrderDetailsService _orderdetails, IOrderInformationsService _orderInformations)
        {
            customers = _customers;
            orders = _orders;
            orderdetails = _orderdetails;
            orderInformations = _orderInformations;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
       [AllowAnonymous]
       [Route("/uyelik/giris")]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/uyelik/giris")]
        public async Task<IActionResult> Login(string KullaniciSifre,string KullaniciAdi)
        {
            bool sonuc = (KullaniciAdi.Contains("@") ? true : false);

            if (sonuc)
            {
                var data = customers.Login(KullaniciAdi, null, KullaniciSifre);
                var claims = new List<Claim>
                {
                    new Claim("ID",data.Data.Id.ToString()),
                    new Claim(ClaimTypes.Name,data.Data.NameSurname),

                };
                var UserIdentity = new ClaimsIdentity(claims, "LoginGiris");
                ClaimsPrincipal principal = new ClaimsPrincipal(UserIdentity);
                var CookiesSuresi = new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddMinutes(120),
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(principal, CookiesSuresi);
                return Redirect("/");

            }
            else
            {
                var data = customers.Login(null, KullaniciAdi, KullaniciSifre);
                var claims = new List<Claim>
                {
                    new Claim("ID",data.Data.Id.ToString()),
                    new Claim(ClaimTypes.Name,data.Data.NameSurname),

                };
                var UserIdentity = new ClaimsIdentity(claims, "LoginGiris");
                ClaimsPrincipal principal = new ClaimsPrincipal(UserIdentity);

                var CookiesSuresi = new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddMinutes(120),
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(principal, CookiesSuresi);
              
                return Redirect("/");
            }
            
        }
        [AllowAnonymous]
        [Route("uyelik/yeni-uye")]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("uyelik/yeni-uye")]
        public async Task<IActionResult> Register(CustomersUpdateDto data)
        
        {
            if (Request.Form["Advert"]== "on")
            {
                data.Advert = true;
            }

            data.Gender = true;
            data.City = "";
            data.District = "";
            data.FullAddress = "";

            if (customers.Add(data).ResultStatus== ResultStatus.Success)
            {
                CustomersDto uye = customers.Login(data.Email, data.Phone, data.Password).Data;
               
                var claims = new List<Claim>
                {
                    new Claim("ID",uye.Id.ToString()),
                    new Claim(ClaimTypes.Name,uye.NameSurname),

                };
                var UserIdentity = new ClaimsIdentity(claims, "LoginGiris");
                ClaimsPrincipal principal = new ClaimsPrincipal(UserIdentity);
                var CookiesSuresi = new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddMinutes(120),
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(principal,CookiesSuresi);
                return Redirect("/");
            }

            else
            {
                TempData["Error"] = "Kayıt Başarısız, Lütfen Tekrar Deneyiniz..";
                return View();
            }
        }
        
        [Route("/hesabim")]
        public IActionResult Account()
        {
            int BulunanUyeId = int.Parse(User.FindFirst(x => x.Type == "ID").Value.ToString());
            return View(customers.GetById(BulunanUyeId).Data);
        }
       
       
        [HttpPost]
        [Route("/hesabim")]
        public async Task<IActionResult> Account(CustomersUpdateDto data)
        {
            int BulunanUyeId = int.Parse(User.FindFirst(x => x.Type == "ID").Value.ToString());
            data.Id = BulunanUyeId;

            var DataMessage = customers.Update(data);
            if (DataMessage.ResultStatus==ResultStatus.Success)
            {
                ViewData["Message"] = "<div class='alert alert-success'>" + DataMessage.Message + "</div>";
            }
            else
            {
                ViewData["Message"] = "<div class='alert alert-danger'>" + DataMessage.Message + "</div>";
            }
            return View(customers.GetById(BulunanUyeId).Data);
        }

        [Route("/siparislerim")]
        public IActionResult Orders(int Id)
        {
            return View(orders.GetByCustomerId(Id).Data);
        }
        [Route("/Cikis")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/uyelik/giris");
        }
    }
}
