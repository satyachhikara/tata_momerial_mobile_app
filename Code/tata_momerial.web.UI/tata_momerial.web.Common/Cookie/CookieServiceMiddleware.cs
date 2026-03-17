using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tata_momerial.web.Common.Cookie
{
    internal class CookieServiceMiddleware
    {
        private readonly RequestDelegate _next;

        public CookieServiceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ICookieService cookieService)
        {
            // write cookies to response right before it starts writing out from MVC/api responses...
            context.Response.OnStarting(() =>
            {
                // cookie service should not write out cookies on 500, possibly others as well
                if (!context.Response.StatusCode.IsInRange(500, 599))
                {
                    cookieService.WriteToResponse(context);
                }
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
