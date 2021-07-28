using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.Catalog.Product;
using ViewModel.Common;

namespace Application.Catalog
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
        Task<PageResult<ProductViewModel>> GetProductPaging(GetProductPagingRequest request);
        //Task<PageResult<Product>> GetProductPaging(GetProductPagingRequest request);
    }
}
