using AutoMapper;
using Souq.core.Interfaces;
using Souq.core.Services;
using Souq.infrastructure.Data;

namespace Souq.infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMapper _mapper;
        private readonly IImageServiceManagement _imageService;
        private readonly AppDbContext _dbContext;

        public IProductRepo ProductRepo { get; }

        public ICategoryRepo CategoryRepo { get; }

        public IImageRepo ImageRepo { get; }
        public UnitOfWork(AppDbContext dbContext, IMapper mapper, IImageServiceManagement imageService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _imageService = imageService;
            ProductRepo = new ProductRepo(_dbContext, _mapper, _imageService);
            CategoryRepo = new CategoryRepo(_dbContext);
            ImageRepo = new ImageRepo(_dbContext);

        }
    }
}
