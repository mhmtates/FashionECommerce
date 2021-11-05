using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DataAccess.Concrete.Contexts.EntityFramework.Mappings
{
    public class TemporaryBasketsMap : IEntityTypeConfiguration<TemporaryBaskets>
    {
        public void Configure(EntityTypeBuilder<TemporaryBaskets> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.ProductsId).IsRequired();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(150);
            builder.Property(c => c.Quantity).IsRequired();
            builder.Property(c => c.Price).IsRequired();
            builder.Property(c => c.Price).HasColumnType("money");
            builder.Property(c => c.VariantName).IsRequired();
            builder.Property(c => c.VariantName).HasMaxLength(50);
            builder.Property(c => c.CustomersId).IsRequired();
            builder.Property(c => c.CookiesId).IsRequired();

            builder.ToTable("TemporaryBaskets");

        }
    }
}
