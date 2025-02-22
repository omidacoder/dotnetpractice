using DotnetPractice.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetPractice.DataAccess.Seeds
{
    public class Seeder
    {
        private readonly RoleManager<Role> _roleManager;
        public Seeder(RoleManager<Role> roleManager) {
            _roleManager = roleManager;
        } 
        public async Task CreateRolesAsync()
        {
            var AdminRole = await _roleManager.FindByNameAsync("Admin");
            if (AdminRole == null)
            {
                await _roleManager.CreateAsync(new Role() { Name = "Admin"});
            }
            var NormalUserRole = await _roleManager.FindByNameAsync("NormalUser");
            if (NormalUserRole == null)
            {
                await _roleManager.CreateAsync(new Role() { Name = "NormalUser" });

            }
        }
        public void CreateRoles()
        {
            var AdminRole = _roleManager.FindByNameAsync("Admin").Result;
            if (AdminRole == null)
            {
                _ = _roleManager.CreateAsync(new Role() { Name = "Admin" }).Result;
            }
            var NormalUserRole = _roleManager.FindByNameAsync("NormalUser").Result;
            if (NormalUserRole == null)
            {
                _ = _roleManager.CreateAsync(new Role() { Name = "NormalUser" }).Result;

            }
        }
    }
}
