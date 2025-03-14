using Souq.core.Interfaces;
using Souq.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public IProductRepo ProductRepo {get;}

        public ICategoryRepo CategoryRepo { get; }

        public IImageRepo ImageRepo  {get;}
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            ProductRepo = new ProductRepo(_dbContext);
            CategoryRepo = new CategoryRepo(_dbContext);
            ImageRepo = new ImageRepo(_dbContext);
        }
    }
}
