using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Contexts.EntityFramework.Mappings
{
    public class ProductImagesMap : IEntityTypeConfiguration<ProductImages>
    {
        public void Configure(EntityTypeBuilder<ProductImages> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.ProductsId).IsRequired();

            builder.HasOne<Products>(p => p.Products).WithMany(x => x.ProductImages).HasForeignKey(x => x.ProductsId);

            builder.ToTable("ProductImages");
        }
    }
}
