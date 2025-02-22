using DotnetPractice.DataAccess.Models;
using DotnetPractice.DataAccess.Repos.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.DataAccess.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly PostgresDbContext _db;
        private readonly UserManager<User> _userManager;
        public UserRepo(PostgresDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<bool> Add(User model)
        {
            await _userManager.CreateAsync(model);
            return true;
        }

        public async Task<bool> Delete(string Id)
        {
            User user = await Get(Id);
            await _db.Users.Where(u => u.Id == Id).ExecuteDeleteAsync();
            return true;
        }

        public async Task<User> Get(string Id) {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                throw new ArgumentException("کاربر مورد نظر یافت نشد");
            }
            return user;
        } 

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _db.Users.OrderByDescending(u => u.CreatedAt).ToListAsync();
        }
        public async Task<bool> Update(string Id, User model)
        {
            User user = await Get(Id);
            user.Name = model.Name;
            user.Email = model.Email;
            _db.Users.Update(model);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task AssingRoleToUser(User user, string Role)
        {
            await _userManager.AddToRoleAsync(user, Role);
        }
    }
}
