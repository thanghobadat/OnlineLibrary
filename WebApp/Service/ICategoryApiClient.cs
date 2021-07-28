using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.Catalog.Category;
using ViewModel.Common;

namespace WebApp.Service
{
    public interface ICategoryApiClient
    {
        Task<PageResult<CategoryViewModel>> GetCategoryPagings(GetCategoryPagingRequest request);
        Task<ApiResult<bool>> CreateCategory(CategoryViewModel request);

        Task<ApiResult<CategoryViewModel>> GetById(int id);

        Task<List<CategoryViewModel>> GetAll();
        Task<ApiResult<bool>> UpdateCategory(int id, CategoryViewModel request);
        Task<ApiResult<bool>> DeleteCategory(int id);


    }
}
