using Core.Entitiess.Abstract;
using System;

namespace Entities.Dto
{
    public class OrdersUpdateDto:IDto
    {
        public int Id { get; set; }
        public int CustomersId { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentType { get; set; }
        public string CargoNumber { get; set; }
        public decimal CargoPrice { get; set; }
        public string OrderStatus { get; set; }
        public decimal TotalPrice { get; set; }
        public byte Kdv { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal CouponPrice { get; set; }
    }
}
