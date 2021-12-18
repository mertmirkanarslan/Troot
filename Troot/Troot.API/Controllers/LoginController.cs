using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Troot.Model;
using Troot.Model.User;
using Troot.Service.User;

namespace Troot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly IUserService userService;

        public LoginController(IMemoryCache _memoryCache, IUserService _userService)
        {
            memoryCache = _memoryCache;
            userService = _userService;
        }

        //Login 
        [HttpPost]
        public General<bool> Login([FromBody] LoginViewModel loginUser)
        {
            General<bool> response = new() { Entity = false };
            General<LoginViewModel> result = userService.Login(loginUser);

            if (result.IsSuccess)
            {
                if (!memoryCache.TryGetValue("LoginUser", out UserViewModel _loginUser))
                {
                    memoryCache.Set("LoginUser", result.Entity);
                }
                response.Entity = true;
                response.IsSuccess = true;
                response.Message = "İşlem başarılı";
            }
            else
            {
                response.ExceptionMessage = "Tekrar giriş yapın";
            }
            return response;
        }
    }

}
