using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductFacilities.Application.Dto;
using ProductFacilities.Application.Interface;
using ProductFacilities.Domain.Entities;
using ProductFacilities.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductFacilities.Application.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(DataContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateProduct(ProductDto product)
        {
            var data = _mapper.Map<Product>(product);
            data.DateCreated = DateTime.Now;
            _context.Products.Add(data);
            await _context.SaveChangesAsync();

            var products = await _context.Products.ToListAsync();
            var totalAmount = 0;
            foreach (var item in products)
            {
                totalAmount += ((int)item.Cost * item.Quantity);
            }

            return totalAmount;
        }

        public async Task DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(w => w.ProductId == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ProductDto>> GetAllProducts(string? search)
        {
            var products = await _context.Products.Where(w => w.ProductName!.Contains(search!) || search == null).ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
