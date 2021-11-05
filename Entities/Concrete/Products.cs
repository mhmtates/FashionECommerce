
using Core.Entitiess.Abstract;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Products : IEntity
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public string Explanation { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public decimal Discount { get; set; }
        public int Stock { get; set; }

        public string MainImage { get; set; }

        public string Keywords { get; set; }
        public bool Status { get; set; }

        public string DeliveryDate { get; set; }

        public int CategoriesId { get; set; }


        public ICollection<Variants> Variants { get; set; }
        public ICollection<ProductImages> ProductImages { get; set; }
        public virtual Categories Categories { get; set; }

    }
}
