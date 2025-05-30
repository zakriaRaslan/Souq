﻿using Souq.core.Dtos;
using Souq.core.Entities.Product;
using Souq.core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.core.Interfaces
{
    public interface IProductRepo:IGenericRepo<Product>
    {
        Task<ReturnProductDto> GetAllAsync(GetAllProductsParams getAllProductsParams);
        Task<bool> AddAsync(AddProductDto addProductDto);
        Task<bool> UpdateAsync(UpdateProductDto updateProductDto);
        Task DeleteAsync(Product product);
    }
}
