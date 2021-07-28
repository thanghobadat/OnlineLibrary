using ViewModel.Common;

namespace ViewModel.Catalog.Document
{
    public class GetCategoryAssignRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
