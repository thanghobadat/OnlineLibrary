using ViewModel.Common;

namespace ViewModel.Catalog.Document
{
    public class GetRequestPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
