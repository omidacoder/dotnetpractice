using DotnetPractice.Common.Dtos.User;
using DotnetPractice.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.BuisinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        public Task<bool> CreateSuperUser(CreateUserDto user);
        public Task<bool> CreateNormalUser(CreateUserDto user);
        public Task<UserDetailsDto> GetUserDetails(string Id);
        public Task<List<UserGeneralDto>> GetAllUsers();
        public Task<bool> UpdateUser(string Id, UpdateUserDto user);
        public Task<bool> DeleteUser(string Id);
    }
}
