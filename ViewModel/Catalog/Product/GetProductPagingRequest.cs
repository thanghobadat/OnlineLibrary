using ViewModel.Common;

namespace ViewModel.Catalog.Product
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
