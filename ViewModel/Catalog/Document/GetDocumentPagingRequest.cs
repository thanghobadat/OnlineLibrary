using ViewModel.Common;

namespace ViewModel.Catalog.Document
{
    public class GetDocumentPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public int? CategoryId { get; set; }
    }
}
