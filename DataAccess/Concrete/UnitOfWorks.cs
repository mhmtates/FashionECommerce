using DataAccess.Abstract;
using DataAccess.Concrete.Contexts.EntityFramework;
using DataAccess.Concrete.Repositories.EntityRepo;

namespace DataAccess.Concrete.Repositories
{
    public class UnitOfWorks : IUnitOfWorks
    {
        private readonly FashionContext context;
        private CategoriesRepository categories;
        private ProductsRepository products;
        private VariantsRepository variant;
        private ProductImagesRepository ProductImages;
        private CustomerRepository customer;
        private OrderDetailsRepository orderdetail;
        private OrderInformationsRepository orderinfo;
        private OrderNotesRepository ordernote;
        private OrdersRepository orders;
        private TemporaryBasketsRepository Tempbasket;
        private UsersAdminRepository useradmin;
        private AutoBasketsRepository autoBasketRepo;
        private SlidesRepository slide;
        public UnitOfWorks(FashionContext _context)
        {
            context = _context;
        }
        public IProductsRepository ProductsRepository => products ?? new ProductsRepository(context);
        public ICategoriesRepository CategoriesRepository => categories ?? new CategoriesRepository(context);
        public IVariantsRepository VariantsRepository => variant ?? new VariantsRepository(context);
        public IProductImagesRepository ProductImagesRepository => ProductImages ?? new ProductImagesRepository(context);
        public ICustomersRepository CustomersRepository => customer ?? new CustomerRepository(context);
        public IOrderDetailsRepository OrderDetailsRepository => orderdetail ?? new OrderDetailsRepository(context);
        public IOrderInformationsRepository OrderInformationsRepository => orderinfo ?? new OrderInformationsRepository(context);
        public IOrderNotesRepository OrderNotesRepository => ordernote ?? new OrderNotesRepository(context);
        public IOrdersRepository OrdersRepository => orders ?? new OrdersRepository(context); // Ternary IF
        public ITemporaryBasketsRepository TemporaryBasketsRepository => Tempbasket ?? new TemporaryBasketsRepository(context);
        public IUsersAdminRepository UsersAdminRepository => useradmin ?? new UsersAdminRepository(context);
        public IAutoBasketsRepository AutoBasketsRepository => autoBasketRepo ?? new AutoBasketsRepository(context);
        public ISlidesRepository SlidesRepository => slide ?? new SlidesRepository(context);
        public void Dispose()
        {
            context.Dispose();
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
