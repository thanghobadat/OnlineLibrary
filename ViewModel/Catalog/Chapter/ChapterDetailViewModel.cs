using System.Collections.Generic;
using ViewModel.Catalog.Document;
using ViewModel.Common;

namespace ViewModel.Catalog.Chapter
{
    public class ChapterDetailViewModel
    {
        public ApiResult<ChapterViewModel> Result { get; set; }
        public List<ChapterViewModel> ListChapters { get; set; }
        public DocumentViewModel CurrentDocument { get; set; }
    }
}
