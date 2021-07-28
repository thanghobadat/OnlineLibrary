using Data.EF;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Catalog.Product;
using ViewModel.Common;

namespace Application.Catalog
{
    public class ProductService : IProductService
    {
        private readonly LibraryDbContext _context;
        public ProductService(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAll()
        {
            var ListProducts = await _context.Products.ToListAsync();
            return ListProducts;
        }

        public async Task<PageResult<ProductViewModel>> GetProductPaging(GetProductPagingRequest request)
        {
            var query = await _context.Products.ToListAsync();
            var ListProducts = query.AsQueryable();


            //var ListProducts = await EfExtensions.ToListAsyncSafe<ProductViewModel>(query.AsQueryable());
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                ListProducts = ListProducts.Where(x => x.Name.Contains(request.Keyword));
            }


            int totalRow = ListProducts.Count();

            var data = ListProducts.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToList();

            var pagedResult = new PageResult<ProductViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };

            return pagedResult;
        }

    }
}
