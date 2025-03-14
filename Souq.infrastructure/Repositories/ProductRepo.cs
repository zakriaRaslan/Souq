using Souq.core.Entities.Product;
using Souq.core.Interfaces;
using Souq.infrastructure.Data;

namespace Souq.infrastructure.Repositories
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        public ProductRepo(AppDbContext context) : base(context)
        {
        }
    }
}
