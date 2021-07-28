using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.Catalog.Document;
using ViewModel.Common;
using ViewModel.System.Users;

namespace Application.Catalog
{
    public interface IDocumentService
    {
        Task<ApiResult<bool>> Create(DocumentRequest request);
        Task<PageResult<DocumentViewModel>> GetAllPaging(GetDocumentPagingRequest request);
        Task<ApiResult<DocumentViewModel>> GetById(int id);
        Task<List<DocumentViewModel>> GetAllDocument();
        Task<ApiResult<bool>> UpdateDocument(int id, DocumentRequest request);
        Task<ApiResult<bool>> DeleteDocument(int id);
        Task<List<DocumentUserViewModel>> GetDocumentUser(string userName);
        Task<ApiResult<DocumentUserViewModel>> GetDocumentUserById(int documentId, Guid userId);
        Task<ApiResult<DocumentImageViewModel>> GetImageByDocumentId(int id);
        Task<ApiResult<DocumentImageViewModel>> GetImageById(int id);
        Task<ApiResult<DocumentViewModel>> GetDocumentByImageId(int Imageid);
        Task<ApiResult<DocumentImageRequest>> DeleteImage(int id);
        Task<ApiResult<DocumentImageRequest>> UpdateImage(int id, DocumentImageUpdateRequest request);

        Task<PageResultAssign<CategoryAssignResult>> GetCategoryAssign(int id, GetCategoryAssignRequest request);
        Task<ApiResult<bool>> AddCategory(CategoryAssignRequest request);
        Task<ApiResult<bool>> DeleteCategory(CategoryAssignRequest request);
        Task<ApiResult<bool>> SendRequire(SendRequiredRequest request);
        Task<PageResult<DocumentRequestViewModel>> GetAllRequestPaging(GetRequestPagingRequest request);
        Task<ApiResult<bool>> AcceptRequest(SendRequiredRequest request);
        Task<ApiResult<bool>> RefuseRequest(SendRequiredRequest request);

        Task<ApiResult<UserViewModel>> GetUserByUserName(string userName);
        Task<ApiResult<bool>> HideDocument(int id);
        Task<ApiResult<bool>> ShowDocument(int id);
        Task<ApiResult<bool>> VoteDocument(UserVoteRequest request);


    }
}
