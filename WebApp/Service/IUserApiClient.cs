using System;
using System.Threading.Tasks;
using ViewModel.Common;
using ViewModel.System.Users;

namespace WebApp.Service
{
    public interface IUserApiClient
    {
        Task<string> Authenticate(LoginRequest request);
        Task<bool> RegisterUser(RegisterRequest registerRequest);
        Task<ApiResult<PageResult<UserViewModel>>> GetUserPagings(GetUserPagingRequest request);
        Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request);
        Task<ApiResult<UserViewModel>> GetById(Guid id, Guid? curentUser);
        Task<ApiResult<bool>> Delete(Guid id);
    }
}
