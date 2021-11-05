﻿using Core.Entitiess.Abstract;


namespace Entities.Concrete
{
    public class ProductImages:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductsId { get; set; }
        public virtual Products Products { get; set; }
    }
}
