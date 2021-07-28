using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.Catalog.Category;
using ViewModel.Common;

namespace Application.Catalog
{
    public interface ICategoryService
    {
        Task<PageResult<CategoryViewModel>> GetCategoryPaging(GetCategoryPagingRequest request);
        Task<ApiResult<CategoryViewModel>> GetById(int id);
        Task<ApiResult<bool>> CreateCategory(CategoryViewModel request);

        Task<ApiResult<bool>> UpdateCategory(int id, CategoryViewModel request);

        Task<ApiResult<bool>> DeleteCategory(int id);
        Task<List<CategoryViewModel>> GetAll();


    }
}
