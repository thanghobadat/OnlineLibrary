using ViewModel.Common;

namespace ViewModel.Catalog.Category
{
    public class GetCategoryPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
