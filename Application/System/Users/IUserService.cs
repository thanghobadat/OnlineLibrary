using System;
using System.Threading.Tasks;
using ViewModel.Common;
using ViewModel.System.Users;

namespace Application.System.Users
{
    public interface IUserService
    {
        Task<string> Authenticate(LoginRequest request);
        Task<bool> Register(RegisterRequest request);
        Task<ApiResult<PageResult<UserViewModel>>> GetUserPaging(GetUserPagingRequest request);
        Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request);
        Task<ApiResult<UserViewModel>> GetById(Guid id, Guid? curentUser);

        Task<ApiResult<bool>> Delete(Guid id);
    }
}
