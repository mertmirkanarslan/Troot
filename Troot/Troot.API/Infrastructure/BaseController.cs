using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Troot.Model.User;

namespace Troot.API.Infrastructure
{
    public class BaseController :ControllerBase
    {
        //memory cache injection ı
        private readonly IMemoryCache memoryCache;
        public BaseController(IMemoryCache _memoryCache)
        {
            memoryCache = _memoryCache;
        }
        public UserViewModel CurrentUser
        {
            get
            {
                return GetCurrentUser();
            }
        }
        private UserViewModel GetCurrentUser()
        {
            var response = new UserViewModel();

            if (memoryCache.TryGetValue("LoginUser", out UserViewModel _loginUser))
            {
                response = _loginUser;
            }
            return response;
        }

    }
}
