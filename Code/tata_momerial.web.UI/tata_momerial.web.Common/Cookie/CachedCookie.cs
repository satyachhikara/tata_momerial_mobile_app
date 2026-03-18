using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tata_momerial.web.common.Cookie
{
    public class CachedCookie
    {
        public string? Name { get; set; }

        public string? Value { get; set; }

        public CookieOptions? Options { get; set; }

        public bool IsDeleted { get; set; }
    }
}
