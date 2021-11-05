using Core.Entitiess.Abstract;

namespace Entities.Dto
{
	public class CategoriesDto : IDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int ParentId { get; set; }
		public bool Status { get; set; }

	}
}
