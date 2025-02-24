using DotnetPractice.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.DataAccess.Repos.Interfaces
{
    public interface IProductRepo : IRepo<Product>
    {
        Task<IEnumerable<Product>> GetUserProducts(string UserId,string? filterName,string? filterDescriptions,double? filterMinPrice, double? filterMaxPrice);
        Task<Product> GetDetails(string Id);
    }
}
