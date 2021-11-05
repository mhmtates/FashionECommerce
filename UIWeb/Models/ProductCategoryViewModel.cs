using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UIWeb.Models
{
    public class ProductCategoryViewModel
    {
        public int Type { get; set; }
        public ProductsDto Product { get; set; }
        public CategoriesDto Category { get; set; }
    }
}
