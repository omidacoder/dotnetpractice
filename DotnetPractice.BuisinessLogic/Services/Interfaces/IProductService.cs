using DotnetPractice.Common.Dtos.Product;
using DotnetPractice.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.BuisinessLogic.Services.Interfaces
{
    public interface IProductService
    {
        public Task<bool> CreateAsync(CreateProductDto productDto,string userId);
        public Task<List<ProductGeneralDto>> GetUserProducts(string userId, ProductFilterDto filters);
        public Task<bool> UpdateAsync(UpdateProductDto productDto,string Id, string userId);
        public Task<bool> DeleteAsync(string Id, string userId);
        public Task<List<ProductGeneralDto>> GetAll();
        public Task<ProductDetailsDto> GetProductDetails(string Id);
    }
}
