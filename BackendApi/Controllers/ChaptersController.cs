using Application.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ViewModel.Catalog.Chapter;
using ViewModel.Exceptions;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChaptersController : ControllerBase
    {
        private readonly IChapterService _chapterService;
        public ChaptersController(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        [HttpGet("GetChapterPaging")]
        public async Task<IActionResult> GetChapterPaging([FromQuery] GetChapterPagingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var chapters = await _chapterService.GetAllPaging(request);
            return Ok(chapters);
        }

        [HttpGet("GetAllChapter")]
        public async Task<IActionResult> GetAllChapter(int id)
        {
            var chapters = await _chapterService.GetAllChapter(id);
            return Ok(chapters);
        }

        [HttpGet("GetChapterById")]
        public async Task<IActionResult> GetChapterById(int id)
        {
            if (id <= 0)
                throw new OnlineLibraryException("Chapter Id must be greater than 0");
            var chapter = await _chapterService.GetById(id);
            return Ok(chapter);
        }

        [HttpGet("GetChapterBySortOrder")]
        public async Task<IActionResult> GetChapterBySortOrder(int sortOrder, int documentId)
        {
            if (sortOrder <= 0)
                throw new OnlineLibraryException("Chapter Id must be greater than 0");
            var chapter = await _chapterService.GetBySortOrder(sortOrder, documentId);
            return Ok(chapter);
        }


        [HttpPost("CreateChapter")]
        public async Task<IActionResult> CreateChapter([FromBody] ChapterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var chapter = await _chapterService.CreateChapter(request);
            return Ok(chapter);
        }

        [HttpPut("UpdateChapter")]
        public async Task<IActionResult> UpdateChapter(int id, [FromBody] ChapterRequest request)
        {
            var chapter = await _chapterService.UpdateChapter(id, request);
            return Ok(chapter);
        }
    }
}
