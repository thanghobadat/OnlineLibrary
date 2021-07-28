using Data.EF;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Catalog.Chapter;
using ViewModel.Common;

namespace Application.Catalog
{
    public class ChapterService : IChapterService
    {
        private readonly LibraryDbContext _context;
        public ChapterService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateChapter(ChapterRequest request)
        {
            var chapter = await _context.Chapters.FirstOrDefaultAsync(x => x.SortOrder == request.SortOrder & x.DocumentId == request.DocumentId);

            if (chapter != null)
            {
                return new ApiErrorResult<bool>("Chapter is exist");
            }
            chapter = new Chapter()
            {
                Name = request.Name,
                Content = request.Content,
                SortOrder = request.SortOrder,
                DocumentId = request.DocumentId
            };

            await _context.Chapters.AddAsync(chapter);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Create unsuccessful");
            }

            return new ApiSuccessResult<bool>();
        }

        public async Task<List<ChapterViewModel>> GetAllChapter(int id)
        {
            var query = await _context.Chapters.Where(x => x.DocumentId == id).ToListAsync();
            return query.Select(x => new ChapterViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Content = x.Content,
                SortOrder = x.SortOrder,
                DocumentId = x.DocumentId

            }).ToList();
        }

        public async Task<PageResult<ChapterViewModel>> GetAllPaging(GetChapterPagingRequest request)
        {
            var addView = await _context.Documents.FindAsync(request.DocumentId);
            addView.View += 1;
            await _context.SaveChangesAsync();
            var query = await _context.Chapters.Where(x => x.DocumentId == request.DocumentId).ToListAsync();
            var chapters = query.AsQueryable();

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                chapters = chapters.Where(x => x.Name.Contains(request.Keyword));
            }


            int totalRow = chapters.Count();

            var data = chapters.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).OrderBy(x => x.SortOrder)
                .Select(x => new ChapterViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Content = x.Content,
                    SortOrder = x.SortOrder,
                    // DocumentId = x.DocumentId
                }).ToList();

            var pagedResult = new PageResult<ChapterViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };

            return pagedResult;
        }

        public async Task<ApiResult<ChapterViewModel>> GetById(int id)
        {
            var chapter = await _context.Chapters.FindAsync(id);

            if (chapter == null)
            {
                return new ApiErrorResult<ChapterViewModel>("Chapter doesn't exits");
            }

            var chapterViewModel = new ChapterViewModel()
            {
                Id = id,
                Name = chapter.Name,
                Content = chapter.Content,
                SortOrder = chapter.SortOrder,
                DocumentId = chapter.DocumentId
            };
            return new ApiSuccessResult<ChapterViewModel>(chapterViewModel);
        }

        public async Task<ApiResult<ChapterViewModel>> GetBySortOrder(int sortOrder, int documentId)
        {
            var chapter = await _context.Chapters.FirstOrDefaultAsync(x => x.SortOrder == sortOrder && x.DocumentId == documentId);

            if (chapter == null)
            {
                return new ApiErrorResult<ChapterViewModel>("Chapter doesn't exits");
            }

            var chapterViewModel = new ChapterViewModel()
            {
                Id = chapter.Id,
                Name = chapter.Name,
                Content = chapter.Content,
                SortOrder = chapter.SortOrder,
                DocumentId = chapter.DocumentId
            };
            return new ApiSuccessResult<ChapterViewModel>(chapterViewModel);
        }

        public async Task<ApiResult<bool>> UpdateChapter(int id, ChapterRequest request)
        {
            var existhapter = await _context.Chapters.AnyAsync(x => x.SortOrder == request.SortOrder && x.Id != id && x.DocumentId == request.DocumentId);
            if (existhapter)
            {
                return new ApiErrorResult<bool>("Chapter is exist, please choose another Sort Order");
            }

            var chapter = await _context.Chapters.FindAsync(id);

            chapter.Name = request.Name;
            chapter.Content = request.Content;
            chapter.SortOrder = request.SortOrder;

            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Update unsuccessful");
            }

            return new ApiSuccessResult<bool>();
        }
    }
}
