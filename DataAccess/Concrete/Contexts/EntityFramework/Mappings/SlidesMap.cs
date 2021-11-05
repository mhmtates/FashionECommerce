using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Contexts.EntityFramework.Mappings
{
    public class SlidesMap : IEntityTypeConfiguration<Slides>
    {
        public void Configure(EntityTypeBuilder<Slides> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(30);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(100);
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Description).HasColumnType("NVARCHAR(MAX)");
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Image).HasColumnType("NVARCHAR(MAX)");
            builder.Property(x => x.Image).IsRequired();
        }
    }
}
