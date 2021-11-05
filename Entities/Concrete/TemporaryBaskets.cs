using Core.Entitiess.Abstract;

namespace Entities.Concrete
{
    public class TemporaryBaskets :IEntity
    {
        public int Id { get; set; }
        public int ProductsId { get; set; }
        public int CookiesId { get; set; }
        public string MainImage { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string VariantName { get; set; }
        public int CustomersId { get; set; }
    }
}
