using basic_ecommerce.Dto;
using basic_ecommerce.Models;

namespace basic_ecommerce.Interfaces
{
    public interface IProductService
    {
        Task<Product?> CreateProduct(Products req);
        Task<Product?> UpdateProduct(UpdateProduct req, Guid Id);
        Task<Product?> DeleteProduct(Guid Id);
        Task<Product?> GetAllProduct(Guid userId);
        Task<Product?> GetProduct(Guid Id);
    }
}
