using Application.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModel.Catalog.Document;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost("CreateDocument")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateDocument([FromForm] DocumentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var document = await _documentService.Create(request);
            return Ok(document);
        }

        [HttpGet("GetDocumentPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetDocumentPagingRequest request)
        {
            var documents = await _documentService.GetAllPaging(request);
            return Ok(documents);
        }



        [HttpGet("GetAllDocument")]
        public async Task<IActionResult> GetAllDocument()
        {
            var documents = await _documentService.GetAllDocument();
            return Ok(documents);
        }

        [HttpGet("GetDocumentUser")]
        public async Task<IActionResult> GetDocumentUser(string userName)
        {
            var documents = await _documentService.GetDocumentUser(userName);
            return Ok(documents);
        }

        [HttpGet("GetAllRequestPaging")]
        public async Task<IActionResult> GetAllRequestPaging([FromQuery] GetRequestPagingRequest request)
        {
            var documents = await _documentService.GetAllRequestPaging(request);
            return Ok(documents);
        }

        [HttpPut("SendRequire")]
        public async Task<IActionResult> SendRequire(SendRequiredRequest request)
        {
            var result = await _documentService.SendRequire(request);
            return Ok(result);
        }

        [HttpPut("AcceptRequest")]
        public async Task<IActionResult> AcceptRequest(SendRequiredRequest request)
        {
            var result = await _documentService.AcceptRequest(request);
            return Ok(result);
        }

        [HttpPut("RefuseRequest")]
        public async Task<IActionResult> RefuseRequest(SendRequiredRequest request)
        {
            var result = await _documentService.RefuseRequest(request);
            return Ok(result);
        }

        [HttpGet("GetUserByUserName")]
        public async Task<IActionResult> GetUserByUserName(string userName)
        {
            var user = await _documentService.GetUserByUserName(userName);
            return Ok(user);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var document = await _documentService.GetById(id);
            return Ok(document);
        }

        [HttpPut("UpdateDocument")]
        public async Task<IActionResult> UpdateDocument(int id, [FromBody] DocumentRequest request)
        {
            var document = await _documentService.UpdateDocument(id, request);
            return Ok(document);
        }

        [HttpDelete("DeleteDocument")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var document = await _documentService.DeleteDocument(id);
            return Ok(document);
        }

        [HttpGet("GetImageByDocumentId")]
        public async Task<IActionResult> GetImageByDocumentId(int id)
        {
            var image = await _documentService.GetImageByDocumentId(id);
            return Ok(image);
        }

        [HttpGet("GetImageById")]
        public async Task<IActionResult> GetImageById(int id)
        {
            var image = await _documentService.GetImageById(id);
            return Ok(image);
        }

        [HttpGet("GetDocumentByImageId")]
        public async Task<IActionResult> GetDocumentByImageId(int Imageid)
        {
            var document = await _documentService.GetDocumentByImageId(Imageid);
            return Ok(document);
        }


        [HttpDelete("DeleteImage")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _documentService.DeleteImage(id);
            return Ok(image);
        }

        [HttpPut("UpdateImage")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateImage(int id, [FromForm] DocumentImageUpdateRequest request)
        {
            var image = await _documentService.UpdateImage(id, request);
            return Ok(image);
        }

        [HttpGet("GetCategoryAssign")]
        public async Task<IActionResult> GetCategoryAssign(int id, [FromQuery] GetCategoryAssignRequest request)
        {
            var categoryAssign = await _documentService.GetCategoryAssign(id, request);
            return Ok(categoryAssign);
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(CategoryAssignRequest request)
        {
            var document = await _documentService.AddCategory(request);
            return Ok(document);
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory([FromQuery] CategoryAssignRequest request)
        {
            var document = await _documentService.DeleteCategory(request);
            return Ok(document);
        }
        [HttpPut("HideDocument")]
        public async Task<IActionResult> HideDocument(HideAndShowDocumentRequest request)
        {
            var result = await _documentService.HideDocument(request.Id);
            return Ok(result);
        }
        [HttpPut("ShowDocument")]
        public async Task<IActionResult> ShowDocument(HideAndShowDocumentRequest request)
        {
            var result = await _documentService.ShowDocument(request.Id);
            return Ok(result);
        }

        [HttpGet("GetDocumentUserById")]
        public async Task<IActionResult> GetDocumentUserById(int documentId, Guid userId)
        {
            var documents = await _documentService.GetDocumentUserById(documentId, userId);
            return Ok(documents);
        }
        [HttpPut("VoteDocument")]
        public async Task<IActionResult> VoteDocument(UserVoteRequest request)
        {
            var result = await _documentService.VoteDocument(request);
            return Ok(result);
        }

    }
}
