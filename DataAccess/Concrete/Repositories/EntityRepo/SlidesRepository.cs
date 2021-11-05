using Core.DataRepository.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Repositories.EntityRepo
{
   public class SlidesRepository:EfEntityRepository<Slides>,ISlidesRepository
    {
        public SlidesRepository(DbContext context):base(context)
        {

        }
    }
}
