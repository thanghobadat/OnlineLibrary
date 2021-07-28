using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.Catalog.Chapter;
using ViewModel.Common;

namespace Application.Catalog
{
    public interface IChapterService
    {
        Task<PageResult<ChapterViewModel>> GetAllPaging(GetChapterPagingRequest request);
        Task<ApiResult<ChapterViewModel>> GetById(int id);
        Task<ApiResult<ChapterViewModel>> GetBySortOrder(int sortOrder, int documentId);
        Task<ApiResult<bool>> CreateChapter(ChapterRequest request);
        Task<ApiResult<bool>> UpdateChapter(int id, ChapterRequest request);
        Task<List<ChapterViewModel>> GetAllChapter(int id);
    }
}
