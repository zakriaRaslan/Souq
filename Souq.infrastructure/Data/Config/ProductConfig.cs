using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Souq.core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.infrastructure.Data.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x=>x.Description).IsRequired();
            builder.Property(x=>x.Price).IsRequired().HasColumnType(typeName:"decimal(18,2)");
            builder.HasData(
                new Product {Id=1 , Description="test" , Name = "test" , CategoryId=1 , Price=12 }
                );
        }
    }
}
    