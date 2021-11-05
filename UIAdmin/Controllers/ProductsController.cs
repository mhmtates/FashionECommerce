using Business.Abstract;
using Core.Results.ComplexTypes;
using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace UIAdmin.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductsController : Controller
    {
        private readonly IProductsService productsdb;
        private readonly IProductImagesService imagesdb;
        public ProductsController(IProductsService _productsdb, IProductImagesService _imagesdb)
        {
            productsdb = _productsdb;
            imagesdb = _imagesdb;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(productsdb.GetAll().Data);
        }
        [Route("/Products/Add")]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [Route("/Products/Add")]
        [HttpPost]
        public async Task<IActionResult> Add(ProductsUpdateDto data, IFormFile Files, IList<IFormFile> MultiFiles)
        {
            var DataKategori = Request.Form["CategoriesId"];
            foreach (var item in DataKategori)
            {
                if (item != "0")
                {
                    data.CategoriesId = Convert.ToInt32(item);
                }
            }
            if (Files != null)
            {
                
                string NewName = Guid.NewGuid() + Path.GetExtension(Files.FileName);
                string KayitYolu = Path.Combine(URLPath.URLFile, $"wwwroot/images/Products/" + NewName);
                Files.CopyTo(new FileStream(KayitYolu, FileMode.Create));
                data.MainImage = NewName;
                if (productsdb.Add(data).ResultStatus == ResultStatus.Success)
                {
                   
                    int BulunanId = productsdb.SearchId(data.Name, data.Price, data.Stock).Data;
                    
                    if (MultiFiles != null)
                    {
                        foreach (var item in MultiFiles)
                        {
                            string NewNameDetail = Guid.NewGuid() + Path.GetExtension(item.FileName);
                            string KayitYoluDetail = Path.Combine(URLPath.URLFile, $"wwwroot/images/Products/detail/" + NewNameDetail);
                            item.CopyTo(new FileStream(KayitYoluDetail, FileMode.Create));
                            ProductImagesDto ProductImages = new ProductImagesDto();
                            ProductImages.ProductsId = BulunanId;
                            ProductImages.Name = NewNameDetail;
                            imagesdb.Add(ProductImages);
                        }
                    }
                    ViewData["Message"] = "<div class='alert alert-success'>Bilgiler Eklendi.</div>";
                }
                else
                {
                    ViewData["Message"] = "<div class='alert alert-danger'>Bir Hata ile Karşılaşıldı Lütfen Tekrar Deneyiniz.</div>";
                }
            }
            else
            {
                ViewData["Message"] = "<div class='alert alert-danger'>Ürün Ana Resmi Seçilmedi..</div>";
            }
            return View();
        }

        [Route("/Products/Update/{Id:int}")]
        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            ViewBag.detayResim = imagesdb.GetAll(Id).Data;
            return View(productsdb.GetById(Id).Data);
        }
        [Route("/Products/Update/{Id:int}")]
        [HttpPost]
        public async Task<IActionResult> Update(int Id, ProductsUpdateDto data, IFormFile Files, IList<IFormFile> MultiFiles)
        {
            if (Request.Form["CategoriesId"] != "0")
            {
                var DataKategori = Request.Form["CategoriesId"];
                foreach (var item in DataKategori)
                {
                    if (item != "0")
                    {
                        data.CategoriesId = Convert.ToInt32(item);
                    }
                }
            }
            else
            {
                data.CategoriesId = Convert.ToInt32(Request.Form["KategoriGizli"]);
            }
            // Detay Resmi Kontrol.
            if (MultiFiles != null)
            {
                foreach (var item in MultiFiles)
                {
                    string NewNameDetail = Guid.NewGuid() + Path.GetExtension(item.FileName);
                    string KayitYoluDetail = Path.Combine(URLPath.URLFile, $"wwwroot/images/Products/detail/" + NewNameDetail);
                    item.CopyTo(new FileStream(KayitYoluDetail, FileMode.Create));
                    ProductImagesDto ProductImages = new ProductImagesDto();
                    ProductImages.ProductsId = Id;
                    ProductImages.Name = NewNameDetail;
                    imagesdb.Add(ProductImages);
                }
            }
            
            if (Files != null)
            {
               

                string NewName = Guid.NewGuid() + Path.GetExtension(Files.FileName);
                string KayitYolu = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/Products/" + NewName);
                Files.CopyTo(new FileStream(KayitYolu, FileMode.Create));
                data.MainImage = NewName;
                if (productsdb.Update(data).ResultStatus == ResultStatus.Success)
                {
                    ViewData["Message"] = "<div class='alert alert-success'>Bilgiler Güncellendi.</div>";
                }
                else
                {
                    ViewData["Message"] = "<div class='alert alert-danger'>Bilgiler Güncellenemedi.</div>";
                }
            }
            else
            {
                
                if (productsdb.Update(data).ResultStatus == ResultStatus.Success)
                {
                    ViewData["Message"] = "<div class='alert alert-success'>Bilgiler Güncellendi.</div>";
                }
                else
                {
                    ViewData["Message"] = "<div class='alert alert-danger'>Bilgiler Güncellenemedi.</div>";
                }
            }
            ViewBag.detayResim = imagesdb.GetAll(Id).Data;
            return View(productsdb.GetById(Id).Data);
        }


        [Route("/Products/Delete/{Id:int}")]
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var Productdata = productsdb.GetById(Id).Data;
            productsdb.Delete(Id);

            System.IO.File.Delete(Path.Combine(URLPath.URLFile, $"wwwroot/images/Products/" + Productdata.MainImage));

            if (imagesdb.GetAll(Productdata.Id).ResultStatus == ResultStatus.Success)
            {
                foreach (var item in imagesdb.GetAll(Productdata.Id).Data)
                {
                    imagesdb.Delete(item.Id);
                    System.IO.File.Delete(Path.Combine(URLPath.URLFile, $"wwwroot/images/Products/detail/" + item.Name));
                }
            }
            return Redirect("/Product");
        }
        [Route("/Products/DeleteImages/{Id:int}")]
        public async Task<JsonResult> DeleteImages(int Id)
        {
            var ImagesData = imagesdb.GetByIdFirst(Id);
            imagesdb.Delete(Id);
            System.IO.File.Delete(Path.Combine(URLPath.URLFile, $"wwwroot/images/Products/detail/" + ImagesData.Name));
            return Json("Resim Silindi.");
        }

    }
}
