using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ViewModel.Catalog.Chapter;
using ViewModel.Exceptions;
using WebApp.Service;

namespace WebApp.Controllers
{
    public class ChapterController : BaseController
    {
        private readonly IChapterApiClient _chapterApiClient;
        private readonly IDocumentApiClient _documentApiClient;
        public ChapterController(IChapterApiClient chapterApiClient, IDocumentApiClient documentApiClient)
        {
            _chapterApiClient = chapterApiClient;
            _documentApiClient = documentApiClient;
        }
        public async Task<IActionResult> Index(int id, string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetChapterPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                DocumentId = id
            };
            var document = await _documentApiClient.GetById(request.DocumentId);

            ViewBag.DocumentName = document.ResultObj.Name;
            ViewBag.DocumentId = document.ResultObj.Id;
            ViewBag.View = document.ResultObj.View;
            var data = await _chapterApiClient.GetChapterPagings(request);
            ViewBag.KeywordChapter = keyword;
            if (TempData["resultChapter"] != null)
            {
                ViewBag.SuccessMsgChapter = TempData["resultChapter"];
            }

            return View(data);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> CreateChapter(int id)
        {
            var document = await _documentApiClient.GetById(id);
            //ViewBag.DocumentId = document.ResultObj.Id;
            ViewBag.DocumentId = id;
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateChapter(ChapterRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _chapterApiClient.CreateChapter(request);
            if (result.IsSuccessed)
            {
                TempData["resultChapter"] = "Create successfull";
                return RedirectToAction("Index", new { id = request.DocumentId });
            }
            throw new OnlineLibraryException(result.Message);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateChapter(int id)
        {
            if (id <= 0)
            {
                throw new OnlineLibraryException("id must be greater than 0");
            }
            var document = await _documentApiClient.GetById(id);
            ViewBag.DocumentId = document.ResultObj.Id;
            var result = await _chapterApiClient.GetChapterById(id);
            if (result.IsSuccessed)
            {
                var chapter = result.ResultObj;
                var updateRequest = new ChapterViewModel()
                {
                    Name = chapter.Name,
                    Content = chapter.Content,
                    SortOrder = chapter.SortOrder,
                    Id = id
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateChapter(ChapterRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _chapterApiClient.UpdateChapter(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["resultChapter"] = "Update successfull";
                return RedirectToAction("Index", new { id = request.DocumentId });
            }
            throw new OnlineLibraryException(result.Message);
        }


        [HttpGet]
        public async Task<IActionResult> DetailChapter(int id)
        {
            if (id <= 0)
            {
                throw new OnlineLibraryException("id must be greater than 0");
            }

            var result = await _chapterApiClient.GetChapterById(id);
            var listChapter = await _chapterApiClient.GetAllChapter(result.ResultObj.DocumentId);
            var currentDocument = await _documentApiClient.GetById(result.ResultObj.DocumentId);

            if (result.ResultObj.SortOrder == 1)
            {
                var chapterNext = await _chapterApiClient.GetChapterBySortOrder(result.ResultObj.SortOrder + 1, result.ResultObj.DocumentId);
                ViewBag.Next = chapterNext.ResultObj.Id;
            }
            else if (result.ResultObj.SortOrder == listChapter.Count)
            {
                var chapterPre = await _chapterApiClient.GetChapterBySortOrder(result.ResultObj.SortOrder - 1, result.ResultObj.DocumentId);
                ViewBag.Pre = chapterPre.ResultObj.Id;
            }
            else
            {
                var chapterNext = await _chapterApiClient.GetChapterBySortOrder(result.ResultObj.SortOrder + 1, result.ResultObj.DocumentId);
                ViewBag.Next = chapterNext.ResultObj.Id;
                var chapterPre = await _chapterApiClient.GetChapterBySortOrder(result.ResultObj.SortOrder - 1, result.ResultObj.DocumentId);
                ViewBag.Pre = chapterPre.ResultObj.Id;
            }


            if (result.IsSuccessed)
            {
                var viewModel = new ChapterDetailViewModel()
                {
                    Result = result,
                    ListChapters = listChapter,
                    CurrentDocument = currentDocument.ResultObj
                };
                return View(viewModel);
            }
            throw new OnlineLibraryException(result.Message);
        }
    }
}
