using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tata_momerial.web.Common.Cookie
{
    public interface ICookieService
    {
        void Delete(string cookieName);

        T Get<T>(string cookieName, bool isBase64 = false) where T : class;

        T GetOrSet<T>(string cookieName, Func<T> setFunc, DateTimeOffset? expiry = null, bool isBase64 = false) where T : class;

        void Set<T>(string cookieName, T data, DateTimeOffset? expiry = null, bool base64Encode = false) where T : class;

        void WriteToResponse(HttpContext context);

    }
}
