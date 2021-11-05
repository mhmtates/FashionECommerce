using Business.Abstract;
using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace UIWeb.Controllers
{
    [AllowAnonymous]
    public class CategoryController : Controller
    {
        private readonly ICategoriesService categoriesdb;
        private readonly IProductsService productsdb;
        private readonly ITemporaryBasketsService basket;
        private readonly IVariantsService variant;
        public CategoryController(ICategoriesService _categoriesdb, IProductsService _productsdb, ITemporaryBasketsService _basket, IVariantsService _variant)
        {
            categoriesdb = _categoriesdb;
            productsdb = _productsdb;
            basket = _basket;
            variant = _variant;
        }

        public IActionResult Index()
        {
            return View();
        }
      [Route("/{GetName}/{id:int}")]
        public IActionResult Detail(string GetName, int id)
        {

            string Siralama = HttpContext.Request.Query["Sira"];


            ViewBag.Kategori = categoriesdb.GetById(id).Data;
            ViewBag.AltKategori = categoriesdb.GetAll(id).Data;
            IList<ProductsDto> products = new List<ProductsDto>();
            
            if (productsdb.KategoriyeGoreUrunGetirme(categoriesdb.GetById(id).Data.Id).Data != null)
            {
                foreach (var c in productsdb.KategoriyeGoreUrunGetirme(categoriesdb.GetById(id).Data.Id).Data)
                {
                    products.Add(c);
                }
            }

            if (categoriesdb.GetAll(id).Data != null)
            {
                
                foreach (var item in categoriesdb.GetAll(id).Data)
                {
                    
                    if (productsdb.KategoriyeGoreUrunGetirme(item.Id).Data != null)
                    {
                        foreach (var c in productsdb.KategoriyeGoreUrunGetirme(item.Id).Data)
                        {
                            products.Add(c);
                        }
                    }
                  
                    if (categoriesdb.GetAll(item.Id).Data != null)
                    {
                        foreach (var a1 in categoriesdb.GetAll(item.Id).Data)
                        {
                            if (productsdb.KategoriyeGoreUrunGetirme(a1.Id).Data != null)
                            {
                                foreach (var c in productsdb.KategoriyeGoreUrunGetirme(a1.Id).Data)
                                {
                                    products.Add(c);
                                }
                            }
                            if (categoriesdb.GetAll(a1.Id).Data != null)
                            {
                                foreach (var a2 in categoriesdb.GetAll(a1.Id).Data)
                                {
                                    if (productsdb.KategoriyeGoreUrunGetirme(a2.Id).Data != null)
                                    {
                                        foreach (var c in productsdb.KategoriyeGoreUrunGetirme(a2.Id).Data)
                                        {
                                            products.Add(c);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (Siralama == "FGA")
            {
                return View(products.OrderBy(x => x.Discount));
            }
            else if (Siralama == "FGAZ")
            {
                return View(products.OrderByDescending(x => x.Discount));
            }
            else if (Siralama == "AZ")
            {
                return View(products.OrderBy(x => x.Name));
            }
            else if (Siralama == "ZA")
            {
                return View(products.OrderByDescending(x => x.Name));
            }
            else
            {
                return View(products);
            }
        }

        public JsonResult SepeteEkle(string Id)
        {
            string Mesaj = "";
            if (variant.GetAll(int.Parse(Id)).Data != null)
            {
                Mesaj = "Bu ürüne ait varyasyonlar olduğu için detay sayfasından seçim yaparak ekleyebilirsiniz.";
            }
            else
            {
                if (Request.Cookies["BasketId"] == null)
                {
                    // Yeni Cookies Üretme Ekranı.
                    int Bulunan = basket.GetByIdAuto(1).Data.UretilenId + 1;// İlk Başta Veritanındaki Cookie'yi alıp üzerine 1 ekliyor.
                    CookieOptions cookie = new CookieOptions();
                    cookie.Expires = DateTime.Now.AddDays(6);
                    Response.Cookies.Append("BasketId", Bulunan.ToString(), cookie);

                    AutoBasketsDto Dto = basket.GetByIdAuto(1).Data;
                    Dto.UretilenId++;
                    basket.AutoBasketUpdate(Dto);
                    // Yeni Cookies Üretme Ekranı.

                    Mesaj = basket.AddUpdate(Convert.ToInt32(Id), Bulunan, new int[0]).Message;
                }
                else
                {
                    int Cookie = Convert.ToInt32(Request.Cookies["BasketId"]);
                    Mesaj = basket.AddUpdate(Convert.ToInt32(Id), Cookie, new int[0]).Message;
                }
            }
            return Json(Mesaj);
        }


    }
}
