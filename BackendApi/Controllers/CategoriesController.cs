using Application.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ViewModel.Catalog.Category;
using ViewModel.Common;
using ViewModel.Exceptions;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetCategoryPaging")]
        public async Task<IActionResult> GetCategoryPaging([FromQuery] GetCategoryPagingRequest request)
        {
            var categories = await _categoryService.GetCategoryPaging(request);
            return Ok(categories);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int categoryId)
        {
            if (categoryId <= 0)
                throw new OnlineLibraryException("Category Id must be greater than 0");
            var category = await _categoryService.GetById(categoryId);
            return Ok(category);
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var category = await _categoryService.CreateCategory(request);
            return Ok(category);
        }

        [HttpPut("UpdateCategory")]
        public async Task<ApiResult<bool>> UpdateCategory(int id, CategoryViewModel request)
        {

            return await _categoryService.UpdateCategory(id, request);
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {

            var category = await _categoryService.DeleteCategory(id);
            return Ok(category);
        }

    }
}
