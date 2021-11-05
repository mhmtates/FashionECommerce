using DataAccess.Concrete.Contexts.EntityFramework.Mappings;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Contexts.EntityFramework
{

    public class FashionContext : DbContext
    {
        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<AutoBaskets> AutoBaskets { get; set; }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=TOSHIBA\SQLEXPRESS;Database=FashionClub;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Bu Sınıfları ayarları baz alarak SQL'E gönderim Yapıyoruz.
            modelBuilder.ApplyConfiguration(new CategoriesMap());
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new OrderDetailsMap());
            modelBuilder.ApplyConfiguration(new OrderInfoMap());
            modelBuilder.ApplyConfiguration(new OrderNotesMap());
            modelBuilder.ApplyConfiguration(new OrdersMap());
            modelBuilder.ApplyConfiguration(new ProductImagesMap());
            modelBuilder.ApplyConfiguration(new ProductsMap());
            modelBuilder.ApplyConfiguration(new TemporaryBasketsMap());
            modelBuilder.ApplyConfiguration(new UsersAdminMap());
            modelBuilder.ApplyConfiguration(new VariantsMap());
            modelBuilder.ApplyConfiguration(new SlidesMap());
        }
    }
}
