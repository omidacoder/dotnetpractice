using DotnetPractice.DataAccess.Models;
using DotnetPractice.DataAccess.Repos.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.DataAccess.Repos
{
    public class ProductRepo : IProductRepo
    {
        private readonly PostgresDbContext _db;
        public ProductRepo(PostgresDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Add(Product model)
        {

            _db.Products.Add(model);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(string Id)
        {
            var product = await Get(Id);
            await _db.Products.Where(p => p.Id == Id).ExecuteDeleteAsync();
            return true;
        }


        public async Task<Product> Get(string Id)
        {
            var product = await _db.Products.FindAsync(Id);
            if (product == null)
            {
                throw new ArgumentException("محصول مورد نظر یافت نشد");
            }
            return product;
        }

        public async Task<IEnumerable<Product>>GetAll()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<bool> Update(string Id, Product model)
        {
            Product product = await Get(Id);
            product.Name = model.Name;
            product.Description = model.Description;
            product.AvailableCount = model.AvailableCount;
            product.UpdatedAt = DateTime.Now;
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Product>> GetUserProducts(string UserId, string? filterName, string? filterDescriptions, double? filterMinPrice, double? filterMaxPrice)
        {
           var query = _db.Products.AsQueryable();
           if (filterName != null) query = query.Where(p => p.Name != null ? p.Name.Contains(filterName) : false);
           if (filterDescriptions != null) query = query.Where(p => p.Description != null ? p.Description.Contains(filterDescriptions) : false);
           if (filterMinPrice != null) query = query.Where(p => p.Price > filterMinPrice);
           if (filterMaxPrice != null) query = query.Where(p => p.Price < filterMaxPrice);
           query = query.Where(p => p.UserId == UserId).OrderByDescending(p => p.CreatedAt);
           return await query.ToListAsync();
        }
        

        public async Task<Product> GetDetails(string Id) => await _db.Products.Where(p => p.Id == Id).Include(p => p.User).FirstOrDefaultAsync();

    }
}
