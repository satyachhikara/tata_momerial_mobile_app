using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tata_momerial.web.common
{
    public class ApplicationConfiguration
    {
        
        public bool allowonlyonesessionperuser { get; set; }
        public string validdocumenttypes { get; set; } = ".pdf,.xls,.xlsx,.doc,.docx,.png,.jpeg,.jpg,.msg,.ppt,.pptx";
        public string validimagetypes { get; set; } = ".png,.jpeg,.jpg,.gif,.svg";
        public decimal uploadcontentlength { get; set; }
        public int pagesize { get; set; }
        public bool requiressl { get; set; }
        public bool requiresessionout { get; set; }
        public bool enabledcustomantiforgerytoken { get; set; }
        public string webappname { get; set; } = string.Empty;
        public bool sendemails { get; set; }
        public string contentrootpath { get; set; } = string.Empty;
        public string connectionstring { get; set; } = string.Empty;
        public string registrationemailsubject { get; set; } = string.Empty;
        public string forgotpasswordemailsubject { get; set; } = string.Empty;
        public string newuseremailsubject { get; set; } = string.Empty;
        public MailSettings mailsettings { get; set; } = new MailSettings();
        public bool ischeckcaptcha { get; set; }
        public string? domainname { get; set; }
    }

    public class MailSettings
    {
        public string mail { get; set; } = string.Empty;
        public string displayname { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string host { get; set; } = string.Empty;
        public int port { get; set; }
        public string sslOption { get; set; } = string.Empty;
    }
}
