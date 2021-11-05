using Core.Entitiess.Abstract;

namespace Entities.Concrete
{
    public class OrderDetails : IEntity
    {
        public int Id { get; set; }
        public int ProductsId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string VariantName { get; set; }
        public int OrdersId { get; set; }
        public virtual Orders Orders { get; set; }
    }
}
