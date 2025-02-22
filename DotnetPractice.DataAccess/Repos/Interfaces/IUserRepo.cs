using DotnetPractice.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.DataAccess.Repos.Interfaces
{
    public interface IUserRepo : IRepo<User>
    {
        public Task AssingRoleToUser(User user, string Role);
    }
}
