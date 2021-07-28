using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ViewModel.Catalog.Category;
using ViewModel.Exceptions;
using WebApp.Service;

namespace WebApp.Controllers
{

    public class CategoryController : BaseController
    {
        private readonly ICategoryApiClient _categoryApiClient;

        public CategoryController(ICategoryApiClient categoryApiClient)
        {
            _categoryApiClient = categoryApiClient;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetCategoryPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _categoryApiClient.GetCategoryPagings(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }

            return View(data);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryViewModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _categoryApiClient.CreateCategory(request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Create successfull";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);


            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if (id <= 0)
            {
                throw new OnlineLibraryException("id must be greater than 0");
            }
            var category = await _categoryApiClient.GetById(id);
            return View(category.ResultObj);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            if (id <= 0)
            {
                throw new OnlineLibraryException("id must be greater than 0");
            }
            var result = await _categoryApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var category = result.ResultObj;
                var updateRequest = new CategoryViewModel()
                {
                    Name = category.Name,
                    Description = category.Description,
                    Id = id
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryViewModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _categoryApiClient.UpdateCategory(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Update successfull";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);


            return View(request);
        }
        [Authorize(Roles = "admin")]
        //[HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id <= 0)
            {
                throw new OnlineLibraryException("id must be greater than 0");
            }

            var result = await _categoryApiClient.DeleteCategory(id);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Delete Successfull";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            return RedirectToAction("Index");
        }
    }
}
