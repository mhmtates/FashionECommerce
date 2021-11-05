using Core.DataRepository.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Repositories
{
    public class VariantsRepository : EfEntityRepository<Variants>, IVariantsRepository
    {
        public VariantsRepository(DbContext context) : base(context)
        {

        }
    }
}
