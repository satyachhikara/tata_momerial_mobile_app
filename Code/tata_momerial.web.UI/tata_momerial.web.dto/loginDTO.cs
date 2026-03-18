using System;
using System.Collections.Generic;
using System.Text;

namespace tata_momerial.web.dto
{
    public class loginDTO : baseDTO
    {
        public string? emailaddress { get; set; }= string.Empty;    
        public string? returnurl { get; set; } = string.Empty;
        public string? password { get; set; } = string.Empty;
        public string? captchaCode { get; set; } = string.Empty;

    }
}
