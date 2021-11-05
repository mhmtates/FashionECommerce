
using Core.Entitiess.Abstract;
using System;

namespace Entities.Dto
{
    public class ProductsDto : IDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MainImage { get; set; }

        public decimal Discount { get; set; }
        public int Stock { get; set; }

        public decimal Price { get; set; }

        public decimal Fiyat => Discount == 0 ? Price : Discount;
        public decimal DiscountRate => Math.Truncate((Price - Discount) / (Price) * 100);

    }
}
