using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Troot.Model.User;

namespace Troot.API.Infrastructure
{
    public class LoginFilter : Attribute, IActionFilter
    {
        private readonly IMemoryCache memoryCache;
        public LoginFilter(IMemoryCache _memoryCache)
        {
            memoryCache = _memoryCache;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!memoryCache.TryGetValue("LoginUser", out UserViewModel _loginUser))
            {
                context.Result = new UnauthorizedObjectResult("Lütfen giriş yapınız");
            }
        }
    }
}
