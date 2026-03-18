using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using tata_momerial.web.BAL;
using tata_momerial.web.dto;
using tata_momerial.web.ui.Business;

namespace tata_momerial.web.ui.Controllers
{
    public partial class HomeController
    {
        [NonAction]
        [AllowAnonymous]
        private bool IsLocalUrl(string url)
        {
            bool isValidURL = false;
            if (string.IsNullOrWhiteSpace(url))
            {
                isValidURL = false;
            }
            else
            {
                Uri objUriRedirectRequest = new Uri(url);
                Uri objUriCurrentRequest = new Uri(Request.GetDisplayUrl());
                if (objUriRedirectRequest.Host.ToLower() == objUriCurrentRequest.Host.ToLower()
                    && objUriRedirectRequest.Scheme.ToLower() == objUriCurrentRequest.Scheme.ToLower())
                    isValidURL = true;

            }
            return isValidURL;
        }
        [NonAction]
        [AllowAnonymous]
        private string GetRandomNumberForPasswordKey()
        {
            var chars = "123456789AaBbCcDdEeFfGgHhJjKkMmNnPpQqRrSsTtUuVvWwXxYyZz123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 16)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        [NonAction]
        private void ClearSession()
        {
            SessionManager.CurrentUserProfile = null;
            CookieService.Delete(SessionConstants.USER_SESSION_COOKIE_NAME);
            Response.Cookies.Delete(SessionConstants.USER_SESSION_COOKIE_NAME);
        }
        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("login")]
        public async Task<IActionResult> login(string returnURL)
        {
            try
            {
                loginDTO _loginDTO = new loginDTO();
                if (SessionManager.CurrentUserProfile != null)
                {
                    //await BALFactory.Instance.usersBAL.UpdateLastLoginTime(SessionManager.CurrentUserProfile);
                    if (!string.IsNullOrWhiteSpace(returnURL))
                        returnURL = returnURL + "/";

                    if (!string.IsNullOrWhiteSpace(returnURL) && IsLocalUrl(returnURL))
                        return Redirect(returnURL);
                    else

                    {
                        return RedirectToAction("dashboard", "dashboard", new { area = "dashboard" });
                    }
                }
                SessionManager.KeyLogin = GetRandomNumberForPasswordKey();
                ViewBag.KeyLogin = SessionManager.KeyLogin;
                _loginDTO.returnurl = returnURL;
                SessionManager.CaptchaKeyLogin = CommonClass.GetRandomText();
                return View("login", _loginDTO);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return RedirectToAction(WebConstants.ActionViewNames.ERROR_ACTION_NAME, WebConstants.ActionViewNames.HOME_CONTROLLER_NAME, new { area = "" });
            }
        }

        private const string msg_Valid_Captcha = "Please enter a valid captcha code shown in image.(कृपया चित्र में दिखाए गए मान्य कैप्चा कोड को दर्ज करें)";
        private const string msg_Login_Validation = "Please enter all required information.(कृपया जरूरी सभी जानकारियां दर्ज करें)";
        private const string msg_Invalid_UserName = "Invalid email address/password.(अमान्य ईमेल पता/पासवर्ड)";
        private const string msg_locked_user = "You have tried with multiple times with in-correct details and your account has been locked. Please try forgot password to unlock your account.(आपने गलत जानकारी के साथ कई बार कोशिश की है और आपका खाता लॉक कर दिया गया है। कृपया अपना खाता अनलॉक करने के लिए पासवर्ड भूल गए का प्रयास करें।)";
    }
}
