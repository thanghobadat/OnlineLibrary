using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.Catalog.Chapter;
using ViewModel.Common;

namespace WebApp.Service
{
    public interface IChapterApiClient
    {
        Task<PageResult<ChapterViewModel>> GetChapterPagings(GetChapterPagingRequest request);
        Task<List<ChapterViewModel>> GetAllChapter(int id);
        Task<ApiResult<ChapterViewModel>> GetChapterById(int id);
        Task<ApiResult<ChapterViewModel>> GetChapterBySortOrder(int sortOrder, int documentId);
        Task<ApiResult<bool>> CreateChapter(ChapterRequest request);

        Task<ApiResult<bool>> UpdateChapter(int id, ChapterRequest request);
    }
}
