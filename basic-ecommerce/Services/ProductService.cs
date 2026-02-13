using basic_ecommerce.Database;
using basic_ecommerce.Dto;
using basic_ecommerce.Interfaces;
using basic_ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace basic_ecommerce.Services
{
    public class ProductService(AppDbContext context) : IProductService
    {
        public async Task<Product?> CreateProduct(Products req)
        {
            var product = new Product();

            product.userId = req.userId;
            product.productName = req.productName;
            product.productContent = req.productContent;
            product.price = req.price;

            context.products.Add(product);

            await context.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> DeleteProduct(Guid productId)
        {
            var product = await context.products.FirstOrDefaultAsync(p => p.Id == productId);

            if (product is null) return null!;

            context.products.Remove(product);

            await context.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> GetAllProduct(Guid userId)
        {
            var result = await context.products.FirstOrDefaultAsync(u => u.userId == userId);

            if (result is null) return null!;

            return result;
        }

        public async Task<Product?> GetProduct(Guid Id)
        {
            var result = await context.products.FirstOrDefaultAsync(u => u.Id == Id);

            if (result is null) return null!;

            return result;
        }

        public async Task<Product?> UpdateProduct(UpdateProduct req, Guid Id)
        {
            var product = await context.products.FirstOrDefaultAsync(p => p.Id == Id);

            if (product is null) return null!;

            product.productName = req.productName;
            product.productContent = req.productContent;
            product.price = (double)req.price;

            await context.SaveChangesAsync();

            return product;
        }
    }
}
