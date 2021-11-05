using Core.Entitiess.Abstract;


namespace Entities.Dto
{
    public class ProductImagesDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductsId { get; set; }
    }
}
