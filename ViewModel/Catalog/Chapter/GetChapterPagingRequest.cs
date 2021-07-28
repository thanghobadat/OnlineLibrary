using ViewModel.Common;

namespace ViewModel.Catalog.Chapter
{
    public class GetChapterPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public int DocumentId { get; set; }
    }
}
