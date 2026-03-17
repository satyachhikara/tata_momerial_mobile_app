using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tata_momerial.web.Common
{
    public class ApplicationConfiguration
    {
        
        public bool AllowOnlyOneSessionPerUser { get; set; }
        public string ValidDocumentTypes { get; set; } = ".pdf,.xls,.xlsx,.doc,.docx,.png,.jpeg,.jpg,.msg,.ppt,.pptx";
        public string ValidImageTypes { get; set; } = ".png,.jpeg,.jpg,.gif,.svg";
        public decimal UploadContentLength { get; set; }
        public int PageSize { get; set; }
        public bool RequireSSL { get; set; }
        public bool RequireSessionOut { get; set; }
        public bool EnabledCustomAntiForgeryToken { get; set; }
        public string WebAppName { get; set; } = string.Empty;
        public bool SendEmails { get; set; }
        public string ContentRootPath { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string registrationemailsubject { get; set; } = string.Empty;
        public string ForgotPasswordEmailSubject { get; set; } = string.Empty;
        public string NewUserEmailSubject { get; set; } = string.Empty;
        public MailSettings MailSettings { get; set; } = new MailSettings();
        public bool IsCheckCaptcha { get; set; }
        public string? DomainName { get; set; }
        public int MFACodeExpiryInMinutes { get; set; } = 30;
        public bool EnableMFA { get; set; }
        public string MFALoginEmailSubject { get; set; } = string.Empty;
    }

    public class MailSettings
    {
        public string Mail { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string sslOption { get; set; } = string.Empty;
    }
}
