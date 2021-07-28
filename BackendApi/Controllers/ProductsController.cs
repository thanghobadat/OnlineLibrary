using Application.Catalog;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.Catalog.Product;


namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : Controller
    {
        private IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAllProduct")]
        public async Task<List<Product>> GetAllProduct()
        {
            return await _productService.GetAll();
        }

        [HttpGet("GetProductPaging")]
        public async Task<IActionResult> GetProductPaging([FromQuery] GetProductPagingRequest request)
        {
            var products = await _productService.GetProductPaging(request);
            return Ok(products);
        }


    }
}
