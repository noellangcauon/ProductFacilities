using ProductFacilities.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductFacilities.Application.Interface
{
    public interface IProductRepository
    {
        Task<List<ProductDto>> GetAllProducts(string? search);
        Task<int> CreateProduct(ProductDto product);
        Task DeleteProduct(int id);
    }
}
