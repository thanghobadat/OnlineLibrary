using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Catalog.Document;
using ViewModel.Exceptions;
using WebApp.Service;

namespace WebApp.Controllers
{
    public class DocumentController : BaseController
    {
        private readonly IDocumentApiClient _documentApiClient;
        private readonly ICategoryApiClient _categoryApiClient;

        public DocumentController(IDocumentApiClient documentApiClient, ICategoryApiClient categoryApiClient)
        {
            _documentApiClient = documentApiClient;
            _categoryApiClient = categoryApiClient;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int? categoryId, int pageIndex = 1, int pageSize = 10)
        {
            var userName = User.Identity.Name;
            var user = await _documentApiClient.GetUserByUserName(userName);
            ViewBag.userId = user.ResultObj.Id;
            var request = new GetDocumentPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                CategoryId = categoryId
            };
            var pageResult = await _documentApiClient.GetAllPaging(request);
            ViewBag.DocUser = await _documentApiClient.GetDocumentUser(userName);
            ViewBag.Keyword = keyword;

            var categories = await _categoryApiClient.GetAll();
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = categoryId.HasValue && categoryId.Value == x.Id
            });


            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }

            return View(pageResult);
        }


        [HttpGet]
        public async Task<IActionResult> DetailDocument(int id)
        {
            if (id <= 0)
            {
                throw new OnlineLibraryException("id must be greater than 0");
            }

            var result = await _documentApiClient.GetById(id);

            if (result.IsSuccessed)
            {
                return View(result.ResultObj);
            }

            throw new OnlineLibraryException(result.Message);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult CreateDocument()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateDocument(DocumentRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _documentApiClient.CreateDocument(request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Create successfull";
                return RedirectToAction("Index");
            }
            throw new OnlineLibraryException(result.Message);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateDocument(int id)
        {
            if (id <= 0)
            {
                throw new OnlineLibraryException("id must be greater than 0");
            }
            var result = await _documentApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var document = result.ResultObj;
                var updateRequest = new DocumentRequest()
                {
                    Name = document.Name,
                    Description = document.Description,
                    Author = document.Author,
                    Id = id
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateDocument(DocumentRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _documentApiClient.UpdateDocument(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Update successfull";
                return RedirectToAction("Index");
            }
            throw new OnlineLibraryException(result.Message);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            if (id <= 0)
            {
                throw new OnlineLibraryException("id must be greater than 0");
            }

            var result = await _documentApiClient.DeleteDocument(id);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Delete Successfull";
                return RedirectToAction("Index");
            }
            throw new OnlineLibraryException(result.Message);
        }


        [HttpGet]
        public async Task<IActionResult> DetailImage(int id)
        {
            if (id <= 0)
            {
                throw new OnlineLibraryException("id must be greater than 0");
            }

            var result = await _documentApiClient.GetImageByDocumentId(id);
            if (TempData["resultImage"] != null)
            {
                ViewBag.SuccessMsgImage = TempData["resultImage"];
            }

            if (result.IsSuccessed)
            {
                return View(result.ResultObj);
            }

            throw new OnlineLibraryException(result.Message);
        }

        //[HttpDelete]
        public async Task<IActionResult> DeleteImage(int id)
        {
            if (id <= 0)
            {
                throw new OnlineLibraryException("id must be greater than 0 ");
            }


            var result = await _documentApiClient.DeleteImage(id);
            if (result.IsSuccessed)
            {
                TempData["resultImage"] = "Delete successfull";
                return RedirectToAction("DetailImage", new { id = result.ResultObj.DocumentId });
            }
            throw new OnlineLibraryException(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateImage(int id)
        {

            if (id <= 0)
            {
                throw new OnlineLibraryException("id must be greater than 0");
            }


            //var currentUrl = Request.Path.ToString();
            //int docId = int.Parse(currentUrl.Split('/').Last());

            var doc = await _documentApiClient.GetDocumentByImageId(id);
            var result = await _documentApiClient.GetImageById(id);
            if (result.IsSuccessed)
            {
                ViewBag.DocId = doc.ResultObj.Id;
                var image = result.ResultObj;
                var updateRequest = new DocumentImageUpdateRequest()
                {
                    Caption = image.Caption,
                    Id = id,
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> UpdateImage(DocumentImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _documentApiClient.UpdateImage(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["resultImage"] = "Update successfull";

                return RedirectToAction("DetailImage", new { id = result.ResultObj.DocumentId });
            }
            throw new OnlineLibraryException(result.Message);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> IndexAssignCategory(int id, string keyword, int pageIndex = 1, int pageSize = 4)
        {
            var request = new GetCategoryAssignRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
            var categoryAssign = await _documentApiClient.GetCategoryAssign(id, request);
            ViewBag.KeywordAssign = keyword;
            if (TempData["resultAssign"] != null)
            {
                ViewBag.SuccessMsgAssign = TempData["resultAssign"];
            }

            return View(categoryAssign);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddCategory(int categoryId, int documentId)
        {
            if (categoryId <= 0 || documentId <= 0)
            {
                throw new OnlineLibraryException("id must be greater than 0 ");
            }


            var assignRequest = new CategoryAssignRequest()
            {
                CategoryId = categoryId,
                DocumentId = documentId
            };
            var result = await _documentApiClient.AddCategory(assignRequest);
            if (result.IsSuccessed)
            {
                TempData["resultAssign"] = "Assign successfull";
                return RedirectToAction("IndexAssignCategory", new { id = documentId });
            }
            throw new OnlineLibraryException(result.Message);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCategory(int documentId, int categoryId)
        {
            if (categoryId <= 0 || documentId <= 0)
            {
                throw new OnlineLibraryException("id must be greater than 0 ");
            }


            var assignRequest = new CategoryAssignRequest()
            {
                CategoryId = categoryId,
                DocumentId = documentId
            };
            var result = await _documentApiClient.DeleteCategory(assignRequest);
            if (result.IsSuccessed)
            {
                TempData["resultAssign"] = "Delete Successfull";
                return RedirectToAction("IndexAssignCategory", new { id = documentId });
            }
            throw new OnlineLibraryException(result.Message);
        }
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> SendRequire(int documentId, Guid userId)
        {



            if (documentId <= 0 || userId == Guid.Empty)
            {
                throw new OnlineLibraryException("Invalid input data");
            }

            var sendRequireRequest = new SendRequiredRequest()
            {
                DocumentId = documentId,
                UserId = userId
            };
            var result = await _documentApiClient.SendRequired(sendRequireRequest);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Send require successfull";
                return RedirectToAction("Index", "Document");
            }
            throw new OnlineLibraryException(result.Message);

        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> IndexRequest(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetRequestPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
            var pageResult = await _documentApiClient.GetAllRequestPaging(request);

            ViewBag.KeywordRequest = keyword;
            if (TempData["resultRequest"] != null)
            {
                ViewBag.SuccessMsgRequest = TempData["resultRequest"];
            }

            return View(pageResult);

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AcceptRequest(int documentId, Guid userId)
        {
            if (documentId <= 0 || userId == Guid.Empty)
            {
                throw new OnlineLibraryException("Invalid input data");
            }

            var sendRequireRequest = new SendRequiredRequest()
            {
                DocumentId = documentId,
                UserId = userId
            };
            var result = await _documentApiClient.AcceptRequest(sendRequireRequest);
            if (result.IsSuccessed)
            {
                TempData["resultRequest"] = "Accept Request successfull";
                return RedirectToAction("IndexRequest", "Document");
            }
            throw new OnlineLibraryException(result.Message);

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> RefuseRequest(int documentId, Guid userId)
        {
            if (documentId <= 0 || userId == Guid.Empty)
            {
                throw new OnlineLibraryException("Invalid input data");
            }

            var sendRequireRequest = new SendRequiredRequest()
            {
                DocumentId = documentId,
                UserId = userId
            };
            var result = await _documentApiClient.RefuseRequest(sendRequireRequest);
            if (result.IsSuccessed)
            {
                TempData["resultRequest"] = "Refure Request successfull";
                return RedirectToAction("IndexRequest", "Document");
            }
            throw new OnlineLibraryException(result.Message);

        }

        [Authorize(Roles = "admin")]
        //[HttpPost]
        public async Task<IActionResult> HideDocument(int id)
        {
            if (id <= 0)
            {
                throw new OnlineLibraryException("id must be > 0");
            }

            var hideRequest = new HideAndShowDocumentRequest()
            {
                Id = id
            };
            var result = await _documentApiClient.HideDocument(hideRequest);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Hide Document successfull";
                return RedirectToAction("Index", "Document");
            }
            throw new OnlineLibraryException(result.Message);

        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ShowDocument(int id)
        {
            if (id <= 0)
            {
                throw new OnlineLibraryException("id must be > 0");
            }

            var showRequest = new HideAndShowDocumentRequest()
            {
                Id = id
            };
            var result = await _documentApiClient.ShowDocument(showRequest);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Show Document successfull";
                return RedirectToAction("Index", "Document");
            }
            throw new OnlineLibraryException(result.Message);

        }

        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<IActionResult> VoteDocument(int documentId, Guid userId)
        {
            if (documentId <= 0 || userId == Guid.Empty)
            {
                throw new OnlineLibraryException("Invalid input data");
            }

            var result = await _documentApiClient.GetDocumentUserById(documentId, userId);
            if (result.IsSuccessed)
            {
                var userVoteVM = new UserVoteRequest()
                {
                    DocumentId = result.ResultObj.DocumentId,
                    UserId = result.ResultObj.UserId,
                    Vote = result.ResultObj.Votes
                };
                return View(userVoteVM);
            }
            return RedirectToAction("Error", "Home");
        }
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> VoteDocument(UserVoteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _documentApiClient.VoteDocument(request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Vote successfull";
                return RedirectToAction("Index", "Document");
            }
            throw new OnlineLibraryException(result.Message);

        }




    }
}
