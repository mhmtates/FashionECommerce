using Core.DataRepository.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Repositories.EntityRepo
{
    public class TemporaryBasketsRepository : EfEntityRepository<TemporaryBaskets>, ITemporaryBasketsRepository
    {
        public TemporaryBasketsRepository(DbContext context) : base(context)
        {

        }
    }
}
