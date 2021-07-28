using Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.Catalog.Document;
using ViewModel.Common;
using ViewModel.System.Users;

namespace WebApp.Service
{
    public interface IDocumentApiClient
    {
        Task<List<DocumentImage>> GetAllImage();
        Task<PageResult<DocumentViewModel>> GetAllPaging(GetDocumentPagingRequest request);
        Task<List<DocumentViewModel>> GetAll();
        Task<ApiResult<DocumentViewModel>> GetById(int id);
        Task<List<DocumentUserViewModel>> GetDocumentUser(string userName);
        Task<ApiResult<DocumentUserViewModel>> GetDocumentUserById(int documentId, Guid userId);
        Task<ApiResult<bool>> CreateDocument(DocumentRequest request);
        Task<ApiResult<bool>> UpdateDocument(int id, DocumentRequest request);
        Task<ApiResult<bool>> DeleteDocument(int id);

        Task<ApiResult<DocumentImageViewModel>> GetImageByDocumentId(int id);
        Task<ApiResult<DocumentImageViewModel>> GetImageById(int id);
        Task<ApiResult<DocumentViewModel>> GetDocumentByImageId(int Imageid);
        Task<ApiResult<DocumentImageRequest>> DeleteImage(int id);
        Task<ApiResult<DocumentImageRequest>> UpdateImage(int id, DocumentImageUpdateRequest request);
        Task<PageResultAssign<CategoryAssignResult>> GetCategoryAssign(int id, GetCategoryAssignRequest request);
        Task<ApiResult<bool>> AddCategory(CategoryAssignRequest request);
        Task<ApiResult<bool>> DeleteCategory(CategoryAssignRequest request);
        Task<ApiResult<bool>> SendRequired(SendRequiredRequest request);
        Task<PageResult<DocumentRequestViewModel>> GetAllRequestPaging(GetRequestPagingRequest request);
        Task<ApiResult<bool>> AcceptRequest(SendRequiredRequest request);
        Task<ApiResult<bool>> RefuseRequest(SendRequiredRequest request);
        Task<ApiResult<UserViewModel>> GetUserByUserName(string userName);

        Task<ApiResult<bool>> HideDocument(HideAndShowDocumentRequest request);
        Task<ApiResult<bool>> ShowDocument(HideAndShowDocumentRequest request);
        Task<ApiResult<bool>> VoteDocument(UserVoteRequest request);
    }
}
