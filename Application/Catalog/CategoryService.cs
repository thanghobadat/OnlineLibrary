using Data.EF;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Catalog.Category;
using ViewModel.Common;

namespace Application.Catalog
{
    public class CategoryService : ICategoryService
    {
        private readonly LibraryDbContext _context;
        public CategoryService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateCategory(CategoryViewModel request)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Name.Contains(request.Name));

            if (category != null)
            {
                return new ApiErrorResult<bool>("Category is exist");
            }

            category = new Category()
            {
                Name = request.Name,
                Description = request.Description
            };
            await _context.Categories.AddAsync(category);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Create unsuccessful");
            }

            return new ApiSuccessResult<bool>();
        }

        public async Task<PageResult<CategoryViewModel>> GetCategoryPaging(GetCategoryPagingRequest request)
        {
            var query = await _context.Categories.ToListAsync();
            var ListCategorys = query.AsQueryable();

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                ListCategorys = ListCategorys.Where(x => x.Name.Contains(request.Keyword));
            }


            int totalRow = ListCategorys.Count();

            var data = ListCategorys.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToList();

            var pagedResult = new PageResult<CategoryViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };

            return pagedResult;
        }

        public async Task<ApiResult<CategoryViewModel>> GetById(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return new ApiErrorResult<CategoryViewModel>("Category doesn't exits");
            }

            var categoryViewModel = new CategoryViewModel()
            {
                Id = id,
                Name = category.Name,
                Description = category.Description
            };
            return new ApiSuccessResult<CategoryViewModel>(categoryViewModel);
        }

        public async Task<ApiResult<bool>> UpdateCategory(int id, CategoryViewModel request)
        {
            if (await _context.Categories.AnyAsync(x => x.Name == request.Name && x.Id != id))
            {
                return new ApiErrorResult<bool>("Category is exist");
            }

            var category = await _context.Categories.FindAsync(id);

            category.Name = request.Name;
            category.Description = request.Description;

            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Update unsuccessful");
            }

            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return new ApiErrorResult<bool>("Category doesn't exist");
            }

            _context.Categories.Remove(category);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Delete unsuccessful");
            }
            return new ApiSuccessResult<bool>();
        }

        public async Task<List<CategoryViewModel>> GetAll()
        {
            var query = await _context.Categories.ToListAsync();
            //var categories = query.AsQueryable();
            return query.Select(x => new CategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();
        }
    }
}
