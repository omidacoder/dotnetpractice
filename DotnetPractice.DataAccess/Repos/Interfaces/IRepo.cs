using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.DataAccess.Repos.Interfaces
{
    public interface IRepo<TModel>
    {
        Task<bool> Add(TModel model);
        Task<bool> Update(string Id,TModel model);
        Task<bool> Delete(string Id);
        Task<IEnumerable<TModel>> GetAll();
        Task<TModel> Get(string Id);
    }
}
