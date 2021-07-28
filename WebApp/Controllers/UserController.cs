using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Common;
using ViewModel.Exceptions;
using ViewModel.System.Users;
using WebApp.Service;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
        private readonly IDocumentApiClient _documentApiClient;
        public UserController(IUserApiClient userApiClient, IConfiguration configuration,
            IDocumentApiClient documentApiClient)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
            _documentApiClient = documentApiClient;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {

            if (!ModelState.IsValid)
                return View(ModelState);

            var token = await _userApiClient.Authenticate(request);

            var userPrincipal = this.ValidateToken(token);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = true
            };
            HttpContext.Session.SetString("Token", token);
            await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        userPrincipal,
                        authProperties);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.RegisterUser(request);
            if (result)
                return RedirectToAction("Index", "Home");

            return View(request);
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetUserPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _userApiClient.GetUserPagings(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }

            return View(data.ResultObj);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {

            ApiResult<UserViewModel> result;
            if (User.IsInRole("user"))
            {
                var userName = User.Identity.Name;
                var currentUser = await _documentApiClient.GetUserByUserName(userName);
                result = await _userApiClient.GetById(id, currentUser.ResultObj.Id);
            }
            else
            {
                result = await _userApiClient.GetById(id, null);
            }

            if (result.IsSuccessed)
            {
                return View(result.ResultObj);

            }
            throw new OnlineLibraryException(result.Message);
        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            ApiResult<UserViewModel> result;
            if (User.IsInRole("user"))
            {
                var userName = User.Identity.Name;
                var currentUser = await _documentApiClient.GetUserByUserName(userName);
                result = await _userApiClient.GetById(id, currentUser.ResultObj.Id);
            }
            else
            {
                result = await _userApiClient.GetById(id, null);

            }

            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new UserUpdateRequest()
                {
                    Dob = user.Dob,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Id = id
                };
                return View(updateRequest);
            }
            throw new OnlineLibraryException(result.Message);
        }



        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.UpdateUser(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Update Successfull";
                return RedirectToAction("Index");
            }


            ModelState.AddModelError("", result.Message);
            return View(request);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var result = await _userApiClient.Delete(id);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Delete Successfull";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            return View();
        }


        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }
    }
}
