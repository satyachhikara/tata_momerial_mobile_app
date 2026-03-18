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
            email.Sender = MailboxAddress.Parse(_mailSettings.mail);
            if (!string.IsNullOrWhiteSpace(mailRequest.toemail))
            {
                mailRequest.toemail = mailRequest.toemail.Replace(";", ",");
                string[] emailAddress = mailRequest.toemail.Split(',');
                if (emailAddress != null
                    && emailAddress.Length > 0)
                {
                    foreach (string emailadd in emailAddress)
                    {
                        email.To.Add(MailboxAddress.Parse(emailadd));
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(mailRequest.ccemail))
            {
                mailRequest.ccemail = mailRequest.ccemail.Replace(";", ",");
                string[] emailAddress = mailRequest.ccemail.Split(',');
                if (emailAddress != null
                    && emailAddress.Length > 0)
                {
                    foreach (string emailadd in emailAddress)
                    {
                        email.Cc.Add(MailboxAddress.Parse(emailadd));
                    }
                }
            }

            email.Subject = mailRequest.subject;
            var builder = new BodyBuilder();
            if (mailRequest.attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.attachments)
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
            if (mailRequest.lstbyteattachments != null && mailRequest.lstbyteattachments.Count > 0)
            {
                foreach (var attachment in mailRequest.lstbyteattachments)
                {
                    builder.Attachments.Add(attachment.FileName, attachment.ByteAttachment);
                }
            }
            builder.HtmlBody = mailRequest.body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.host, _mailSettings.port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.mail, _mailSettings.password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }


    public class MailRequest
    {
        public string toemail { get; set; } = string.Empty;
        public string ccemail { get; set; } = string.Empty;
        public string subject { get; set; } = string.Empty;
        public string body { get; set; } = string.Empty;
        public List<MailAttachments>? lstbyteattachments { get; set; }
        public List<IFormFile>? attachments { get; set; }


    }
    public class MailAttachments
    {
        public string FileName { get; set; } = string.Empty;
        public byte[]? ByteAttachment { get; set; } 
    }
}
