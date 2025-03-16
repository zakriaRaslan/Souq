using Microsoft.AspNetCore.Http;
using Souq.core.Entities.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.core.Dtos
{
    public record ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public string  CategoryName { get; set; }
        public virtual List<ImageDto> Images { get; set; }
    }
    public record ImageDto
    {
        public string ImageName { get; set; }
        public int ProductId { get; set; }
    }

    public record AddProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public int CategoryId { get; set; }
        public IFormFileCollection Images { get; set; }
    }

    public record UpdateProductDto:AddProductDto
    {
        public int Id { get; set; }
    }
}
