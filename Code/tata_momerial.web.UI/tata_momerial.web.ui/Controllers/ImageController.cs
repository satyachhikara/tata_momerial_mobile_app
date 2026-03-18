using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Drawing;
using tata_momerial.web.common;
using tata_momerial.web.common.Cookie;
using tata_momerial.web.ui.Business;

namespace tata_momerial.web.ui.Controllers
{
    public class ImageController : BaseController
    {
        public ImageController(ICookieService objCookieService, ICompositeViewEngine viewEngine,
       ILogger<BaseController> logger, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IMailService _mailService) : base(objCookieService, viewEngine, env, _mailService, logger)
        {

        }

        [HttpGet("tata-momerial-captcha-login/{s}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public FileResult GetCaptchaImageLogin(string s)
        {
            if (!string.IsNullOrWhiteSpace(s) && s.IndexOf("refresh") >= 0)
            {
                SessionManager.CaptchaKeyLogin = CommonClass.GetRandomText();
            }
            if (SessionManager.CaptchaKeyLogin != null)
            {
                string text = SessionManager.CaptchaKeyLogin;
                return GetCaptchaImage(text);
            }
            return null;
        }

        [HttpGet("tata-momerial-captcha-forgot-password/{s}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public FileResult GetCaptchaImageForgotPassword(string s)
        {
            if (!string.IsNullOrWhiteSpace(s) && s.IndexOf("refresh") >= 0)
            {
                SessionManager.CaptchaKeyForgotPassword = CommonClass.GetRandomText();
            }
            if (SessionManager.CaptchaKeyForgotPassword != null)
            {
                string text = SessionManager.CaptchaKeyForgotPassword;
                return GetCaptchaImage(text);
            }
            return null;
        }

        [HttpGet("tata-momerial-captcha-reset-password/{s}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public FileResult GetCaptchaImageReSetPassword(string s)
        {
            if (!string.IsNullOrWhiteSpace(s) && s.IndexOf("refresh") >= 0)
            {
                SessionManager.CaptchaKeyReSetPassword = CommonClass.GetRandomText();
            }
            if (SessionManager.CaptchaKeyReSetPassword != null)
            {
                string text = SessionManager.CaptchaKeyReSetPassword;
                return GetCaptchaImage(text);
            }
            return null;
        }

        [HttpGet("tata-momerial-captcha-change-password/{s}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public FileResult GetCaptchaImageChangePassword(string s)
        {
            if (!string.IsNullOrWhiteSpace(s) && s.IndexOf("refresh") >= 0)
            {
                SessionManager.CaptchaKeyChangePassword = CommonClass.GetRandomText();
            }
            if (SessionManager.CaptchaKeyChangePassword != null)
            {
                string text = SessionManager.CaptchaKeyChangePassword;
                return GetCaptchaImage(text);
            }
            return null;
        }

        [NonAction]
        public FileResult GetCaptchaImage(string text)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            Font font = new Font("Arial", 15);
            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width + 40, (int)textSize.Height + 20);
            drawing = Graphics.FromImage(img);

            Color backColor = Color.SeaShell;
            Color textColor = Color.Red;
            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 20, 10);

            drawing.Save();

            font.Dispose();
            textBrush.Dispose();
            drawing.Dispose();

            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            img.Dispose();

            return File(ms.ToArray(), "image/png");
        }
        [NonAction]
        private IActionResult NoAttachmentFound()
        {
            string filePath = Path.Combine(WebConstants.NoAttachmentPDF);
            byte[] bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, "application/pdf");
        }
    }
}
