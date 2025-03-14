using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.core.Interfaces
{
    public interface IUnitOfWork
    {
        public IProductRepo ProductRepo { get; }
        public ICategoryRepo CategoryRepo { get; }
        public IImageRepo ImageRepo { get; }
    }
}
