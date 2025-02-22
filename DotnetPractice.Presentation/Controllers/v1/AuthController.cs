using DotnetPractice.BuisinessLogic.Services;
using DotnetPractice.BuisinessLogic.Services.Interfaces;
using DotnetPractice.Common.Dtos.Auth;
using DotnetPractice.Common.Utils;
using DotnetPractice.DataAccess;
using DotnetPractice.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotnetPractice.Presentation.Controllers.v1
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;
        public AuthController(UserManager<User> userManager,TokenSettings tokenSettigns, PostgresDbContext postgresDbContext)
        {
            _userManager = userManager;
            _authService = new AuthService(tokenSettigns, postgresDbContext, userManager);
        }
        [HttpPost("/auth/login")]
        public async Task<IActionResult> Login([FromBody]LoginDto loginInfo)
        {
                try
                {
                    var result = await _authService.GenerateNewTokenAsync(loginInfo);
                    
                    //var authUser = await _userManager.FindByNameAsync(loginInfo.UserName);
                    //var roles = await _userManager.GetRolesAsync(authUser);

                    return Ok(result);
                }
                catch (Exception ex) 
                {
                    if(ex.Message == "Unauthorized")
                    return Unauthorized("خطا در ورود");
                    else return BadRequest(ex.Message);
                }
                
        }
    }
}
