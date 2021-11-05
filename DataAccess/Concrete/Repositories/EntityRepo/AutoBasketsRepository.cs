using Core.DataRepository.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
namespace DataAccess.Concrete.Repositories.EntityRepo
{
    public class AutoBasketsRepository : EfEntityRepository<AutoBaskets>, IAutoBasketsRepository
    {
        public AutoBasketsRepository(DbContext context) : base(context)
        {

        }
    }
}
