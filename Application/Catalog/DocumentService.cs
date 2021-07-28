using Application.Common;
using Data.EF;
using Data.Entities;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ViewModel.Catalog.Document;
using ViewModel.Common;
using ViewModel.Exceptions;
using ViewModel.System.Users;

namespace Application.Catalog
{
    public class DocumentService : IDocumentService
    {
        private readonly LibraryDbContext _context;
        private readonly IStorageService _storageService;
        public DocumentService(LibraryDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task<ApiResult<bool>> Create(DocumentRequest request)
        {
            var document = new Document()
            {
                Name = request.Name,
                Description = request.Description,
                Author = request.Author,
                DateCreated = DateTime.Now
            };

            if (request.ThumbnailImage != null)
            {
                document.DocumentImage = new DocumentImage()
                {
                    Caption = request.ThumbnailImage.FileName,
                    DateCreated = DateTime.Now,
                    FizeSize = request.ThumbnailImage.Length,
                    ImagePath = await this.SaveFile(request.ThumbnailImage)
                };
            }
            else
            {
                document.DocumentImage = new DocumentImage()
                {
                    Caption = "Default Image",
                    DateCreated = DateTime.Now,
                    FizeSize = 8174,
                    ImagePath = "dface13b-c68f-449b-a4ef-4f405eeeb299.jpg"
                };
            }


            await _context.Documents.AddAsync(document);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Create unsuccessful");
            }

            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<DocumentImageViewModel>> GetImageByDocumentId(int id)
        {
            if (id <= 0)
            {
                return new ApiErrorResult<DocumentImageViewModel>("Document id must be greater than 0");
            }
            var image = await _context.DocumentImages.FirstOrDefaultAsync(x => x.DocumentId == id);
            if (image == null)
            {
                return new ApiErrorResult<DocumentImageViewModel>("Image doesn't exits");
            }

            var imageViewModel = new DocumentImageViewModel()
            {
                Id = image.Id,
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                FizeSize = image.FizeSize,
                ImagePath = image.ImagePath,
            };
            return new ApiSuccessResult<DocumentImageViewModel>(imageViewModel);
        }

        public async Task<ApiResult<DocumentImageViewModel>> GetImageById(int id)
        {
            if (id <= 0)
            {
                return new ApiErrorResult<DocumentImageViewModel>("Image id must be greater than 0");
            }
            var image = await _context.DocumentImages.FirstOrDefaultAsync(x => x.Id == id);
            if (image == null)
            {
                return new ApiErrorResult<DocumentImageViewModel>("Image doesn't exits");
            }

            var imageViewModel = new DocumentImageViewModel()
            {
                Id = image.Id,
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                FizeSize = image.FizeSize,
                ImagePath = image.ImagePath,
            };
            return new ApiSuccessResult<DocumentImageViewModel>(imageViewModel);

        }

        public async Task<ApiResult<DocumentViewModel>> GetDocumentByImageId(int Imageid)
        {
            if (Imageid <= 0)
            {
                return new ApiErrorResult<DocumentViewModel>("Image id must be greater than 0");
            }
            var image = await _context.DocumentImages.FirstOrDefaultAsync(x => x.Id == Imageid);
            if (image == null)
            {
                return new ApiErrorResult<DocumentViewModel>($"Doesn't contain an image with the id {Imageid}");
            }

            var document = await _context.Documents.FirstOrDefaultAsync(x => x.Id == image.DocumentId);

            var documentVM = new DocumentViewModel()
            {
                Id = document.Id,
                Name = document.Name,
                Description = document.Description,
                Author = document.Author,
                DateCreated = document.DateCreated,
                View = document.View,
                IsShow = document.IsShow
            };
            return new ApiSuccessResult<DocumentViewModel>(documentVM);

        }


        public async Task<ApiResult<DocumentImageRequest>> DeleteImage(int id)
        {
            var image = await _context.DocumentImages.FindAsync(id);

            if (image == null)
            {
                return new ApiErrorResult<DocumentImageRequest>("Image doesn't exits");
            }
            var docImageVM = new DocumentImageRequest()
            {
                DocumentId = image.DocumentId
            };
            if (image.ImagePath != "f496a8bb-0371-45ee-bcf7-30a3ae3cd273.jpg")
            {
                await _storageService.DeleteFileAsync(image.ImagePath);

            }
            else
            {
                throw new OnlineLibraryException("Can't delete Default Image");
            }
            image.Caption = "Default Image";
            image.DateCreated = DateTime.Now;
            image.FizeSize = 8174;
            image.ImagePath = "f496a8bb-0371-45ee-bcf7-30a3ae3cd273.jpg";
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<DocumentImageRequest>("Delete Fail");
            }
            return new ApiSuccessResult<DocumentImageRequest>(docImageVM);

        }

        public async Task<ApiResult<DocumentImageRequest>> UpdateImage(int id, DocumentImageUpdateRequest request)
        {
            var image = await _context.DocumentImages.FindAsync(id);

            if (image == null)
            {
                return new ApiErrorResult<DocumentImageRequest>("Image doesn't exits");
            }
            var docImageVM = new DocumentImageRequest()
            {
                DocumentId = image.DocumentId
            };
            if (image.ImagePath != "f496a8bb-0371-45ee-bcf7-30a3ae3cd273.jpg")
            {
                await _storageService.DeleteFileAsync(image.ImagePath);

            }

            if (request.Caption == null)
            {
                image.Caption = request.ThumbnailImage.FileName;
            }
            image.Caption = request.Caption;
            image.DateCreated = DateTime.Now;
            image.FizeSize = request.ThumbnailImage.Length;
            image.ImagePath = await this.SaveFile(request.ThumbnailImage);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<DocumentImageRequest>("Delete Fail");
            }
            return new ApiSuccessResult<DocumentImageRequest>(docImageVM);

        }

        public async Task<PageResult<DocumentViewModel>> GetAllPaging(GetDocumentPagingRequest request)
        {
            var query = from d in _context.Documents
                        join di in _context.DocumentImages on d.Id equals di.DocumentId
                        join dic in _context.DocumentCategories on d.Id equals dic.DocumentId into ddic
                        from dic in ddic.DefaultIfEmpty()
                        join c in _context.Categories on dic.CategoryId equals c.Id into cdic
                        from c in cdic.DefaultIfEmpty()
                        select new { d, dic, c, di };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.d.Name.Contains(request.Keyword));
            }
            if (request.CategoryId != null && request.CategoryId != 0)
            {
                query = query.Where(p => p.dic.CategoryId == request.CategoryId);
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)./*OrderByDescending(x => x.d.Name)*/
                .Select(x => new DocumentViewModel()
                {
                    Id = x.d.Id,
                    Name = x.d.Name,
                    DateCreated = x.d.DateCreated,
                    Description = x.d.Description,
                    Author = x.d.Author,
                    View = x.d.View,
                    IsShow = x.d.IsShow,
                    ImagePath = x.di.ImagePath
                }).ToListAsync();

            var result = data.GroupBy(x => x.Id).Select(g => g.First()).ToList();

            var pagedResult = new PageResult<DocumentViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = result
            };
            return pagedResult;
        }

        public async Task<ApiResult<DocumentViewModel>> GetById(int id)
        {
            var document = await _context.Documents.Include(x => x.DocumentImage).FirstOrDefaultAsync(x => x.Id == id);

            if (document == null)
            {
                return new ApiErrorResult<DocumentViewModel>("Document doesn't exits");
            }

            var documentViewModel = new DocumentViewModel()
            {
                Id = id,
                Name = document.Name,
                Description = document.Description,
                Author = document.Author,
                DateCreated = document.DateCreated,
                View = document.View,
                IsShow = document.IsShow,
                ImagePath = document.DocumentImage.ImagePath
            };
            return new ApiSuccessResult<DocumentViewModel>(documentViewModel);
        }

        public async Task<ApiResult<bool>> UpdateDocument(int id, DocumentRequest request)
        {
            if (await _context.Documents.AnyAsync(x => x.Name == request.Name && x.Id != id))
            {
                return new ApiErrorResult<bool>("Document is exist");
            }

            var document = await _context.Documents.FindAsync(id);

            document.Name = request.Name;
            document.Description = request.Description;
            document.Author = request.Author;

            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Update unsuccessful");
            }

            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> DeleteDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            //var document = await _context.Documents.Include(x => x.Docu).FindAsync(id);
            if (document == null)
            {
                return new ApiErrorResult<bool>("Document doesn't exist");
            }
            var image = await GetImageByDocumentId(id);
            await DeleteImage(image.ResultObj.Id);

            _context.Documents.Remove(document);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Delete unsuccessful");
            }
            return new ApiSuccessResult<bool>();
        }




        //Save File
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<PageResultAssign<CategoryAssignResult>> GetCategoryAssign(int id, GetCategoryAssignRequest request)
        {
            var currentDocument = _context.Documents.FirstOrDefault(x => x.Id == id);
            if (currentDocument == null)
            {
                throw new OnlineLibraryException("Document doesn't exists");
            }

            var queryExistsCategory = await _context.DocumentCategories.Where(x => x.DocumentId == currentDocument.Id).ToListAsync();
            var existsCategory = queryExistsCategory.AsQueryable();


            var queryNonExitsCategory = await _context.Categories.ToListAsync();
            var nonExistsCategory = queryNonExitsCategory.AsQueryable();

            var DocumentVM = new DocumentAssignViewModel()
            {
                Id = currentDocument.Id,
                Name = currentDocument.Name
            };
            var existsCategoryVM = existsCategory.Select(x => new CategoryAssignViewModel()
            {
                Id = x.CategoryId,
                Name = x.Category.Name,
                Description = x.Category.Description
            }).ToList();


            var querynonExistsCategoryVM = nonExistsCategory.Select(x => new CategoryAssignViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();

            var queryNonExists = querynonExistsCategoryVM.Where(xn => existsCategoryVM.All(x => x.Id != xn.Id));
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                queryNonExists = queryNonExists.Where(x => x.Name.Contains(request.Keyword));
            }

            var totalRow = queryNonExists.Count();
            var nonExistsCategoryVM = queryNonExists.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).ToList();



            var categoryAssign = new CategoryAssignResult()
            {
                Document = DocumentVM,
                ExistsCategory = existsCategoryVM,
                NonExistCategory = nonExistsCategoryVM
            };

            var result = new PageResultAssign<CategoryAssignResult>()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalRecords = totalRow,
                item = categoryAssign
            };

            return result;
        }

        public async Task<ApiResult<bool>> AddCategory(CategoryAssignRequest request)
        {
            var documentCategory = new DocumentCategory()
            {
                DocumentId = request.DocumentId,
                CategoryId = request.CategoryId
            };
            _context.DocumentCategories.Add(documentCategory);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Add category unsuccessful");
            }
            return new ApiSuccessResult<bool>();

        }

        public async Task<ApiResult<bool>> DeleteCategory(CategoryAssignRequest request)
        {
            var document = await _context.DocumentCategories.FindAsync(request.DocumentId, request.CategoryId);
            if (document == null)
            {
                return new ApiErrorResult<bool>("Category doesn't exist in Document");
            }

            _context.DocumentCategories.Remove(document);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Delete unsuccessful");
            }
            return new ApiSuccessResult<bool>();
        }

        public async Task<List<DocumentViewModel>> GetAllDocument()
        {
            var query = await _context.Documents.ToListAsync();
            return query.Select(x => new DocumentViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Author = x.Author,
                DateCreated = x.DateCreated,
                View = x.View

            }).ToList();
        }

        public async Task<List<DocumentUserViewModel>> GetDocumentUser(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            var query = await _context.DocumentUsers.Where(x => x.UserId == user.Id).ToListAsync();
            return query.Select(x => new DocumentUserViewModel()
            {
                UserId = x.UserId,
                DocumentId = x.DocumentId,
                ExpirationDate = x.ExpirationDate,
                RequestExtension = x.RequestExtension
            }).ToList();
        }

        public async Task<ApiResult<bool>> SendRequire(SendRequiredRequest request)
        {

            var query = await _context.DocumentUsers.FirstOrDefaultAsync(x => x.DocumentId == request.DocumentId && x.UserId == request.UserId);
            int result;
            if (query == null)
            {
                query = new DocumentUser()
                {
                    DocumentId = request.DocumentId,
                    UserId = request.UserId,
                    RequestExtension = true,
                    ExpirationDate = DateTime.Now.AddDays(-1)
                };
                await _context.DocumentUsers.AddAsync(query);
                result = await _context.SaveChangesAsync();
            }
            else
            {
                query.RequestExtension = true;
                result = await _context.SaveChangesAsync();
            }


            if (result == 0)
            {
                return new ApiErrorResult<bool>("Send require fail!!");
            }
            return new ApiSuccessResult<bool>();
        }

        public async Task<PageResult<DocumentRequestViewModel>> GetAllRequestPaging(GetRequestPagingRequest request)
        {
            var query = from du in _context.DocumentUsers
                        join d in _context.Documents on du.DocumentId equals d.Id
                        join u in _context.Users on du.UserId equals u.Id
                        select new { du, u, d };
            query = query.Where(x => x.du.RequestExtension == true);

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.u.UserName.Contains(request.Keyword) || x.u.FirstName.Contains(request.Keyword) || x.d.Name.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new DocumentRequestViewModel()
                {
                    DocumentId = x.du.DocumentId,
                    UserId = x.du.UserId,
                    ExpirationDate = x.du.ExpirationDate,
                    DocumentName = x.d.Name,
                    UserName = x.u.UserName,
                    FirstName = x.u.FirstName,
                    LastName = x.u.LastName,

                }).ToListAsync();


            var pagedResult = new PageResult<DocumentRequestViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }

        public async Task<ApiResult<bool>> AcceptRequest(SendRequiredRequest request)
        {
            var query = await _context.DocumentUsers.FirstOrDefaultAsync(x => x.DocumentId == request.DocumentId && x.UserId == request.UserId);

            query.ExpirationDate = DateTime.Now.AddDays(7);
            query.RequestExtension = false;
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Accept request fail!!");
            }
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> RefuseRequest(SendRequiredRequest request)
        {
            var query = await _context.DocumentUsers.FirstOrDefaultAsync(x => x.DocumentId == request.DocumentId && x.UserId == request.UserId);

            query.RequestExtension = false;
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("not required!!");
            }

            var user = await _context.Users.FindAsync(request.UserId);
            string to = user.Email;
            string subject = "Request failed";
            string body = "Your request to view the document was not accepted. Please try again next time.";
            var mailSetting = await _context.MailSettings.FirstOrDefaultAsync();

            //thiết lập nội dung gởi mail
            var email = new MimeMessage();

            email.Sender = new MailboxAddress(mailSetting.DisplayName, mailSetting.EMail);

            email.From.Add(new MailboxAddress(mailSetting.DisplayName, mailSetting.EMail));
            email.To.Add(new MailboxAddress(to, to));
            email.Subject = subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();

            // tạo 1 smtp
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                //kết nối máy chủ
                await smtp.ConnectAsync(mailSetting.Host, mailSetting.Port, SecureSocketOptions.StartTls);
                // xác thực
                await smtp.AuthenticateAsync(mailSetting.EMail, mailSetting.Password);
                //gởi
                await smtp.SendAsync(email);
            }
            catch (Exception e)
            {
                return new ApiErrorResult<bool>("Error: " + e.Message);
            }

            //ngắt kết nối tới máy chủ
            smtp.Disconnect(true);

            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<UserViewModel>> GetUserByUserName(string userName)
        {
            if (userName == null)
            {
                return new ApiErrorResult<UserViewModel>("UserName is require ");
            }
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                return new ApiErrorResult<UserViewModel>($"Doesn't find an user with the username {userName}");
            }

            var userVM = new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Dob = user.DoB,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };
            return new ApiSuccessResult<UserViewModel>(userVM);
        }

        public async Task<ApiResult<bool>> HideDocument(int id)
        {
            var query = await _context.Documents.FindAsync(id);
            if (query == null)
            {
                return new ApiErrorResult<bool>("Document doesn't exist");
            }

            query.IsShow = false;

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                return new ApiErrorResult<bool>("Document is hidden");
            }

            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> ShowDocument(int id)
        {
            var query = await _context.Documents.FindAsync(id);
            if (query == null)
            {
                return new ApiErrorResult<bool>("Document doesn't exist");
            }

            query.IsShow = true;

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                return new ApiErrorResult<bool>("Document is Showing");
            }

            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<DocumentUserViewModel>> GetDocumentUserById(int documentId, Guid userId)
        {
            var query = await _context.DocumentUsers.FirstOrDefaultAsync(x => x.DocumentId == documentId && x.UserId == userId);
            if (query == null)
            {
                return new ApiErrorResult<DocumentUserViewModel>("don't find");
            }

            var result = new DocumentUserViewModel()
            {
                Votes = query.Votes,
                DocumentId = query.DocumentId,
                UserId = query.UserId,
                ExpirationDate = query.ExpirationDate,
                RequestExtension = query.RequestExtension
            };
            return new ApiSuccessResult<DocumentUserViewModel>(result);

        }

        public async Task<ApiResult<bool>> VoteDocument(UserVoteRequest request)
        {
            var query = await _context.DocumentUsers.FirstOrDefaultAsync(x => x.DocumentId == request.DocumentId && x.UserId == request.UserId);
            if (query == null)
            {
                return new ApiErrorResult<bool>("don't find");
            }
            query.Votes = request.Vote;
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Vote document unsuccess");
            }
            return new ApiSuccessResult<bool>();
        }


        public async Task<PageResult<DocumentViewModel>> GetDocUserPaging(GetDocumentPagingRequest request)
        {
            var query = from d in _context.Documents
                        join di in _context.DocumentImages on d.Id equals di.DocumentId
                        join dic in _context.DocumentCategories on d.Id equals dic.DocumentId into ddic
                        from dic in ddic.DefaultIfEmpty()
                        join c in _context.Categories on dic.CategoryId equals c.Id into cdic
                        from c in cdic.DefaultIfEmpty()
                        select new { d, dic, c, di };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.d.Name.Contains(request.Keyword));
            }
            if (request.CategoryId != null && request.CategoryId != 0)
            {
                query = query.Where(p => p.dic.CategoryId == request.CategoryId);
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new DocumentViewModel()
                {
                    Id = x.d.Id,
                    Name = x.d.Name,
                    DateCreated = x.d.DateCreated,
                    Description = x.d.Description,
                    Author = x.d.Author,
                    View = x.d.View,
                    IsShow = x.d.IsShow,
                    ImagePath = x.di.ImagePath
                }).ToListAsync();

            var result = data.GroupBy(x => x.Id).Select(g => g.First()).ToList();

            var pagedResult = new PageResult<DocumentViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = result
            };
            return pagedResult;
        }
    }
}
