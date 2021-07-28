using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.Catalog.Product;
using ViewModel.Common;

namespace WebApp.Service
{
    public interface IProductApiClient
    {
        Task<List<Product>> GetAllProduct();
        Task<PageResult<ProductViewModel>> GetProductPagings(GetProductPagingRequest request);
    }
}
