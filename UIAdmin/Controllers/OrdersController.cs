using Business.Abstract;
using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UIAdmin.Controllers
{
    [Authorize(Roles = "admin,temsilci")]
    public class OrdersController : Controller
    {
        private readonly IOrdersService orders;
        private readonly IOrderInformationsService orderInformations;

        public OrdersController(IOrdersService _orders, IOrderInformationsService _orderInformations)
        {
            orders = _orders;
            orderInformations = _orderInformations;
        }
        public async Task<IActionResult> Index()
        {
            return View(orders.GetAll(null).Data);
        }
        [Route("/Orders/Detail/{Id:int?}")]
        public async Task<IActionResult> Detail(int Id)
        {
            return View(orders.FiveTableGetAll(Id).Data);
        }
        [HttpPost]
        [Route("/Orders/Detail/{Id:int?}")]
        public async Task<IActionResult> Detail(int Id, OrderInformationsDto data)
        {
            data.Sms = (Request.Form["Sms"] == "1" ? true : false);
            data.Email = (Request.Form["Email"] == "1" ? true : false);
            data.InfoDate = DateTime.Now;
            data.OrdersId = Id;
            data.Id = 0;
            data.CustomersId = orders.FiveTableGetAll(Id).Data.FirstOrDefault().CustomersId;
            orderInformations.Add(data);

            return View(orders.FiveTableGetAll(Id).Data);
        }
        public async Task<IActionResult> Teslim()
        {
            return View(orders.GetAll("Teslim Edildi").Data);
        }

        public async Task<IActionResult> İptal()
        {
            return View(orders.GetAll("İptal Edildi").Data);
        }
        public async Task<IActionResult> İade()
        {
            return View(orders.GetAll("İade Edildi").Data);
        }

    }
}
