using Core.DataRepository.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Repositories
{
    public class ProductImagesRepository : EfEntityRepository<ProductImages>, IProductImagesRepository
    {
        public ProductImagesRepository(DbContext context) : base(context)
        {

        }
    }
}
