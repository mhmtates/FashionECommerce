using Core.Entitiess.Abstract;

namespace Entities.Dto
{
    public class VariantsDto : IDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string Name { get; set; }
        public int ProductsId { get; set; }
        public decimal Price { get; set; }
    }
}
