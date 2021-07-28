using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ViewModel.Catalog.Product;
using WebApp.Service;

namespace WebApp.Controllers
{
    //[Authorize(Roles = "user")]
    public class ProductController : BaseController
    {
        private readonly IProductApiClient _productApiClient;

        public ProductController(IProductApiClient productApiClient)
        {
            _productApiClient = productApiClient;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 1)
        {
            var request = new GetProductPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _productApiClient.GetProductPagings(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }

            return View(data);
        }

    }
}
