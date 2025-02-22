using AutoMapper;
using DotnetPractice.BuisinessLogic.Services.Interfaces;
using DotnetPractice.Common.Dtos.Product;
using DotnetPractice.Common.Dtos.User;
using DotnetPractice.DataAccess;
using DotnetPractice.DataAccess.Models;
using DotnetPractice.DataAccess.Repos;
using DotnetPractice.DataAccess.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.BuisinessLogic.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        public ProductService(PostgresDbContext db, IMapper mapper) {
            _productRepo = new ProductRepo(db);
            _mapper = mapper;
        }
        public async Task<bool> CreateAsync(CreateProductDto productDto, string userId)
        {
            var product = _mapper.Map<Product>(productDto);
            product.UserId= userId;
            product.Id= Guid.NewGuid().ToString();
            await _productRepo.Add(product);
            return true;
        }

        public async Task<bool> DeleteAsync(string Id, string userId)
        {
            // check the permission
            var product = await _productRepo.Get(Id);
            if (product == null)
            {
                throw new Exception("محصول مورد نظر یافت نشد");
            }
            if(product.UserId != userId)
            {
                throw new Exception("شما به این محصول دسترسی ندارید");
            }
            await _productRepo.Delete(Id);
            return true;
        }

        public async Task<List<ProductGeneralDto>> GetAll()
        {
            var products = await _productRepo.GetAll();
            return _mapper.Map<List<ProductGeneralDto>>(products);
        }

        public async Task<ProductDetailsDto> GetProductDetails(string Id)
        {
            var product = await _productRepo.GetDetails(Id);
            if (product == null)
            {
                throw new Exception("محصول مورد نظر یافت نشد");
            }
            //Console.WriteLine("Here is the print");
            //Console.WriteLine(product.User.Id);
            return _mapper.Map<ProductDetailsDto>(product);

        }

        public async Task<List<ProductGeneralDto>> GetUserProducts(string userId)
        {
            var products = await _productRepo.GetUserProducts(userId);
            return _mapper.Map<List<ProductGeneralDto>>(products);
        }

        public async Task<bool> UpdateAsync(UpdateProductDto productDto, string Id, string userId)
        {
            // check the permission
            var product = await _productRepo.Get(Id);
            if (product == null)
            {
                throw new Exception("محصول مورد نظر یافت نشد");
            }
            if (product.UserId != userId)
            {
                throw new Exception("شما به این محصول دسترسی ندارید");
            }
            await _productRepo.Update(Id,_mapper.Map<Product>(productDto));
            return true;
        }
    }
}
