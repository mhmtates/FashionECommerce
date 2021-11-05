using Core.Entitiess.Abstract;

namespace Entities.Dto
{
	public class TemporaryBasketsDto : IDto
    {
        public int Id { get; set; }
        public int CookiesId { get; set; }
        public int ProductsId { get; set; }
        public string MainImage { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string VariantName { get; set; }
        public int CustomersId { get; set; }
    }
}
