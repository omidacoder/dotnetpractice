using AutoMapper;
using DotnetPractice.BuisinessLogic.Services;
using DotnetPractice.Common.Dtos;
using DotnetPractice.Common.Dtos.User;
using DotnetPractice.Common.Utils;
using DotnetPractice.DataAccess;
using DotnetPractice.DataAccess.Enums;
using DotnetPractice.DataAccess.Models;
using DotnetPractice.DataAccess.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetPractice.Presentation.Controllers.v1
{
    public class UserController : Controller
    {
        private readonly UserService _service;
        public UserController(IMapper mapper, PostgresDbContext db, UserManager<User> userManager)
        {
            _service = new UserService(mapper, db,userManager);
        }
        // GET: UserController
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("/users")]
        public async Task<ActionResult> Index()
        {
            var result = await _service.GetAllUsers();
            GeneralResponse<List<UserGeneralDto>> response = new GeneralResponse<List<UserGeneralDto>> { status = DataAccess.Enums.ResponseStatusEnum.Success, data=result };
            return Ok(response);
        }

        // GET: UserController/Details/5
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("/user/{id}")]
        public async Task<ActionResult> Details(string Id)
        {
            try
            {
                var result = await _service.GetUserDetails(Id);
                GeneralResponse<UserDetailsDto> response = new GeneralResponse<UserDetailsDto> { status = DataAccess.Enums.ResponseStatusEnum.Success, data = result };
                return Ok(response);
            }
            catch (Exception ex)
            {
                GeneralResponse<object> response = new GeneralResponse<object>{ status = ResponseStatusEnum.Error, message=ex.Message, data=null };
                return BadRequest(response);
            }
            
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("/user/{id}")]
        public async Task<ActionResult> Delete(string Id)
        {
            try
            {
                await _service.DeleteUser(Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                GeneralResponse<object> response = new GeneralResponse<object> { status = ResponseStatusEnum.Error, message = ex.Message, data = null };
                return BadRequest(response);
            }

        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("/user/{id}")]
        public async Task<ActionResult> Update(string Id, [FromBody]UpdateUserDto user)
        {
            try
            {
                await _service.UpdateUser(Id, user);
                return NoContent();
            }
            catch (Exception ex)
            {
                GeneralResponse<object> response = new GeneralResponse<object> { status = ResponseStatusEnum.Error, message = ex.Message, data = null };
                return BadRequest(response);
            }

        }

        // below endpoint is for test purpose only and will not be available in production
        [HttpPost("/super-user")]
        public async Task<IActionResult> CreateSuperUser([FromBody]CreateUserDto user)
        {
             var result = await _service.CreateSuperUser(user);
             return NoContent();
        }

        [HttpPost("/register-user")]
        public async Task<IActionResult> CreateNormalUser([FromBody] CreateUserDto user)
        {
            var result = await _service.CreateNormalUser(user);
            return NoContent();
        }

    }
}
