using System;

namespace DataAccess.Abstract
{
    public interface IUnitOfWorks:IDisposable
    {
        IProductsRepository ProductsRepository { get; }
        ICategoriesRepository CategoriesRepository { get; }
        IVariantsRepository VariantsRepository { get; }
        IProductImagesRepository ProductImagesRepository { get; }
        ICustomersRepository CustomersRepository { get; }
        IOrderDetailsRepository OrderDetailsRepository { get; }
        IOrderInformationsRepository OrderInformationsRepository { get; }
        IOrderNotesRepository OrderNotesRepository { get; }
        IOrdersRepository OrdersRepository { get; }
        ITemporaryBasketsRepository TemporaryBasketsRepository { get; }
        IUsersAdminRepository UsersAdminRepository { get; }
        IAutoBasketsRepository AutoBasketsRepository { get; }
        ISlidesRepository SlidesRepository { get; }
        void SaveChanges();
    }
}
