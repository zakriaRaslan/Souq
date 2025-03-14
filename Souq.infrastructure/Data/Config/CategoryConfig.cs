using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Souq.core.Entities.Product;

namespace Souq.infrastructure.Data.Config
{
    public class CategoryConfig:IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(40);
            builder.Property(x => x.Description).IsRequired();
            builder.HasData(
                new Category {Id=1 , Name="test" ,Description="test"}
                );
        }
    }
}
