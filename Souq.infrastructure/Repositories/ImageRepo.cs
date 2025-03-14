using Souq.core.Entities.Product;
using Souq.core.Interfaces;
using Souq.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.infrastructure.Repositories
{
    public class ImageRepo : GenericRepo<Image>, IImageRepo

    {
        public ImageRepo(AppDbContext context) : base(context)
        {
        }
    }
}
