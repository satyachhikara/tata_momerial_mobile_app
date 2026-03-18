using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;

namespace tata_momerial.web.common
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            if (!string.IsNullOrWhiteSpace(mailRequest.ToEmail))
            {
                mailRequest.ToEmail = mailRequest.ToEmail.Replace(";", ",");
                string[] emailAddress = mailRequest.ToEmail.Split(',');
                if (emailAddress != null
                    && emailAddress.Length > 0)
                {
                    foreach (string emailadd in emailAddress)
                    {
                        email.To.Add(MailboxAddress.Parse(emailadd));
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(mailRequest.CcEmail))
            {
                mailRequest.CcEmail = mailRequest.CcEmail.Replace(";", ",");
                string[] emailAddress = mailRequest.CcEmail.Split(',');
                if (emailAddress != null
                    && emailAddress.Length > 0)
                {
                    foreach (string emailadd in emailAddress)
                    {
                        email.Cc.Add(MailboxAddress.Parse(emailadd));
                    }
                }
            }

            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            if (mailRequest.LstByteAttachments != null && mailRequest.LstByteAttachments.Count > 0)
            {
                foreach (var attachment in mailRequest.LstByteAttachments)
                {
                    builder.Attachments.Add(attachment.FileName, attachment.ByteAttachment);
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }


    public class MailRequest
    {
        public string ToEmail { get; set; } = string.Empty;
        public string CcEmail { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public List<MailAttachments>? LstByteAttachments { get; set; }
        public List<IFormFile>? Attachments { get; set; }


    }
    public class MailAttachments
    {
        public string FileName { get; set; } = string.Empty;
        public byte[]? ByteAttachment { get; set; }
    }
}
