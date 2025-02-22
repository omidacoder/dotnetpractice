using AutoMapper;
using DotnetPractice.BuisinessLogic.Services;
using DotnetPractice.BuisinessLogic.Services.Interfaces;
using DotnetPractice.Common.Dtos.User;
using DotnetPractice.Common.Dtos;
using DotnetPractice.DataAccess;
using DotnetPractice.DataAccess.Enums;
using DotnetPractice.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DotnetPractice.Common.Dtos.Product;

namespace DotnetPractice.Presentation.Controllers.v1
{
    public class ProductController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IProductService _productService;
        public ProductController(PostgresDbContext db, UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _productService = new ProductService(db,mapper);
        }
        [HttpGet("/user/products")]
        [Authorize(Policy = "Access")]
        public async Task<IActionResult> UserProducts()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var products = await _productService.GetUserProducts(user.Id);
                GeneralResponse<List<ProductGeneralDto>> response = new GeneralResponse<List<ProductGeneralDto>> { status = DataAccess.Enums.ResponseStatusEnum.Success, data =products };
                return Ok(response);
            }
            catch (Exception ex)
            {
                GeneralResponse<object> response = new GeneralResponse<object> { status = ResponseStatusEnum.Error, message = ex.Message, data = null };
                return BadRequest(response);
            }
        }
        [HttpPost("/product")]
        [Authorize(Policy = "Access")]
        public async Task<IActionResult> Create([FromBody]CreateProductDto createProduct)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                await _productService.CreateAsync(createProduct, user.Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                GeneralResponse<object> response = new GeneralResponse<object> { status = ResponseStatusEnum.Error, message = ex.Message, data = null };
                return BadRequest(response);
            }
        }

        [HttpDelete("/product/{id}")]
        [Authorize(Policy = "Access")]
        public async Task<IActionResult> Create(string Id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                await _productService.DeleteAsync(Id,user.Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                GeneralResponse<object> response = new GeneralResponse<object> { status = ResponseStatusEnum.Error, message = ex.Message, data = null };
                return BadRequest(response);
            }
        }

        [HttpPut("/product/{id}")]
        [Authorize(Policy = "Access")]
        public async Task<IActionResult> Create([FromBody]UpdateProductDto updateProduct,string Id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                await _productService.UpdateAsync(updateProduct,Id, user.Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                GeneralResponse<object> response = new GeneralResponse<object> { status = ResponseStatusEnum.Error, message = ex.Message, data = null };
                return BadRequest(response);
            }
        }

        [HttpGet("/product/{id}")]
        [Authorize(Policy = "Access")]
        public async Task<IActionResult> Details(string Id)
        {
            try
            {
                var product = await _productService.GetProductDetails(Id);
                GeneralResponse<ProductDetailsDto> response = new GeneralResponse<ProductDetailsDto> { status = DataAccess.Enums.ResponseStatusEnum.Success, data = product };
                return Ok(response);
            }
            catch (Exception ex)
            {
                GeneralResponse<object> response = new GeneralResponse<object> { status = ResponseStatusEnum.Error, message = ex.Message, data = null };
                return BadRequest(response);
            }
        }

    }
}
