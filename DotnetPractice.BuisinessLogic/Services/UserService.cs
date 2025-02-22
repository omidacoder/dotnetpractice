using AutoMapper;
using DotnetPractice.BuisinessLogic.Services.Interfaces;
using DotnetPractice.Common.Dtos.User;
using DotnetPractice.Common.Utils;
using DotnetPractice.DataAccess;
using DotnetPractice.DataAccess.Models;
using DotnetPractice.DataAccess.Repos;
using DotnetPractice.DataAccess.Repos.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.BuisinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public UserService(IMapper mapper, PostgresDbContext dbContext,UserManager<User> userManager)
        {
            _userRepo = new UserRepo(dbContext,userManager);
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> CreateSuperUser(CreateUserDto SuperUser)
        {
            User user = _mapper.Map<User>(SuperUser);
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user,SuperUser.Password);
            var result = await _userRepo.Add(user);
            await _userRepo.AssingRoleToUser(user, "Admin");
            return result;
        }

        public async Task<bool> CreateNormalUser(CreateUserDto SuperUser)
        {
            User user = _mapper.Map<User>(SuperUser);
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, SuperUser.Password);
            var result = await _userRepo.Add(user);
            await _userRepo.AssingRoleToUser(user, "NormalUser");
            return result;
        }

        public async Task<List<UserGeneralDto>> GetAllUsers()
        {
            return _mapper.Map<List<UserGeneralDto>>(await _userRepo.GetAll());
        }

        public async Task<UserDetailsDto> GetUserDetails(string Id)
        {
            return _mapper.Map<UserDetailsDto>(await _userRepo.Get(Id));
        }
        public async Task<bool> UpdateUser(string Id, UpdateUserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            return await _userRepo.Update(Id, user);
        }
        public async Task<bool> DeleteUser(string Id)
        {
            return await _userRepo.Delete(Id);
        }
    }
}
