using Microsoft.AspNetCore.Http;
using tata_momerial.web.common.Cookie;
using tata_momerial.web.common;
using tata_momerial.web.dto;

namespace tata_momerial.web.ui.Business
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
        }
        public static decimal GetDecimal(this ISession session, string key)
        {
            var value = session.GetString(key);
            decimal decValue = 0;
            decimal.TryParse(value, out decValue);
            return decValue;
        }
        public static long GetLong(this ISession session, string key)
        {
            var value = session.GetString(key);
            long longValue = 0;
            long.TryParse(value, out longValue);
            return longValue;
        }
        public static int GetInt(this ISession session, string key)
        {
            var value = session.GetString(key);
            int intValue = 0;
            int.TryParse(value, out intValue);
            return intValue;
        }
        public static void SetDecimal(this ISession session, string key, decimal decValue)
        {
            session.SetString(key, decValue.ToString());
        }
        public static void SetLong(this ISession session, string key, long longValue)
        {
            session.SetString(key, longValue.ToString());
        }

        public static DateTime GetDateTime(this ISession session, string key)
        {
            var value = session.GetString(key);
            DateTime dtValue = DateTime.MinValue;
            DateTime.TryParse(value, out dtValue);
            return dtValue;
        }
        public static void SetDateTime(this ISession session, string key, DateTime dtValue)
        {
            session.SetString(key, dtValue.ToString());
        }
    }
    public class SessionManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ApplicationConfiguration _applicationConfiguration => AppServicesHelper.ApplicationConfiguration;


        private readonly ICookieService _cookieService;

        private readonly CommonUtilityClass _commonUtilityClass;
        public SessionManager(ICookieService cookieService)
        {
            _httpContextAccessor = tata_momerial.web.common.AppServicesHelper.HttpContextAccessor;
            _cookieService = cookieService;
            _commonUtilityClass = new CommonUtilityClass(tata_momerial.web.common.AppServicesHelper.DataProtector);
        }
        public string KeyLogin
        {
            get
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null
                      && _httpContextAccessor.HttpContext.Session != null)
                {
                    return _httpContextAccessor.HttpContext.Session.GetString(SessionConstants.KEY_LOGIN);
                }
                else
                    return string.Empty;
            }
            set
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null
                    && _httpContextAccessor.HttpContext.Session != null
                    && string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext.Session.GetString(SessionConstants.KEY_LOGIN)))
                {
                    _httpContextAccessor.HttpContext.Session.SetString(SessionConstants.KEY_LOGIN, value);
                }
            }
        }
        public string CaptchaKeyLogin
        {
            get
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null
                      && _httpContextAccessor.HttpContext.Session != null)
                {
                    return _httpContextAccessor.HttpContext.Session.GetString(SessionConstants.CAPTCHA_KEY_LOGIN);
                }
                else
                    return string.Empty;
            }
            set
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null
                     && _httpContextAccessor.HttpContext.Session != null)
                {
                    _httpContextAccessor.HttpContext.Session.SetString(SessionConstants.CAPTCHA_KEY_LOGIN, value);
                }
            }
        }
        public string KeyReSetPassword
        {
            get
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null
                        && _httpContextAccessor.HttpContext.Session != null)
                {
                    return _httpContextAccessor.HttpContext.Session.GetString(SessionConstants.KEY_RESET_PASSWORD);
                }
                else
                    return string.Empty;
            }
            set
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null
                     && _httpContextAccessor.HttpContext.Session != null)
                {
                    _httpContextAccessor.HttpContext.Session.SetString(SessionConstants.KEY_RESET_PASSWORD, value);
                }

            }
        }
        public string CaptchaKeyReSetPassword
        {
            get
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null
                        && _httpContextAccessor.HttpContext.Session != null)
                {
                    return _httpContextAccessor.HttpContext.Session.GetString(SessionConstants.CAPTCHA_KEY_RESET_PASSWORD);
                }
                else
                    return string.Empty;
            }
            set
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null
                     && _httpContextAccessor.HttpContext.Session != null)
                {
                    _httpContextAccessor.HttpContext.Session.SetString(SessionConstants.CAPTCHA_KEY_RESET_PASSWORD, value);
                }

            }
        }
        public string CaptchaKeyChangePassword
        {
            get
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null
                       && _httpContextAccessor.HttpContext.Session != null)
                {
                    return _httpContextAccessor.HttpContext.Session.GetString(SessionConstants.CAPTCHA_KEY_CHANGE_PASSWORD);
                }
                else
                    return string.Empty;
            }
            set
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null
                     && _httpContextAccessor.HttpContext.Session != null)
                {
                    _httpContextAccessor.HttpContext.Session.SetString(SessionConstants.CAPTCHA_KEY_CHANGE_PASSWORD, value);
                }

            }
        }
        public string KeyChangePassword
        {
            get
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null
                       && _httpContextAccessor.HttpContext.Session != null)
                {
                    return _httpContextAccessor.HttpContext.Session.GetString(SessionConstants.KEY_CHANGE_PASSWORD);
                }
                else
                    return string.Empty;
            }
            set
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null
                     && _httpContextAccessor.HttpContext.Session != null
                     && string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext.Session.GetString(SessionConstants.KEY_CHANGE_PASSWORD)))
                {
                    _httpContextAccessor.HttpContext.Session.SetString(SessionConstants.KEY_CHANGE_PASSWORD, value);
                }

            }
        }
        public long ResetPasswordUserID
        {
            get
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null
                  && _httpContextAccessor.HttpContext.Session != null)
                {
                    return _httpContextAccessor.HttpContext.Session.GetLong(SessionConstants.RESET_PASSWORD_USER_ID);
                }
                return 0;
            }
            set
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null
                  && _httpContextAccessor.HttpContext.Session != null)
                {
                    _httpContextAccessor.HttpContext.Session.SetLong(SessionConstants.RESET_PASSWORD_USER_ID, value);
                }

            }
        }


        public string CaptchaKeyForgotPassword
        {
            get
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null
                       && _httpContextAccessor.HttpContext.Session != null)
                {
                    return _httpContextAccessor.HttpContext.Session.GetString(SessionConstants.CAPTCHA_KEY_FORGOT_PASSWORD);
                }
                else
                    return string.Empty;
            }
            set
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null
                     && _httpContextAccessor.HttpContext.Session != null)
                {
                    _httpContextAccessor.HttpContext.Session.SetString(SessionConstants.CAPTCHA_KEY_FORGOT_PASSWORD, value);
                }

            }
        }

        public usersDTO? CurrentUserProfile
        {
            get
            {
                usersDTO? _usersDTO = null;
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null
               && _httpContextAccessor.HttpContext.Session != null)
                {
                    _usersDTO = _httpContextAccessor.HttpContext.Session.Get<usersDTO>(SessionConstants.USER_SESSION);
                }
                return _usersDTO;
            }
            set
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null
               && _httpContextAccessor.HttpContext.Session != null)
                {
                    _httpContextAccessor.HttpContext.Session.Set<usersDTO>(SessionConstants.USER_SESSION, value);
                }
            }
        }

        public usersDTO? CurrentUserProfileOTP
        {
            get
            {
                usersDTO? _usersDTO = null;
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null
               && _httpContextAccessor.HttpContext.Session != null)
                {
                    _usersDTO = _httpContextAccessor.HttpContext.Session.Get<usersDTO>(SessionConstants.USER_SESSION_OTP);
                }
                return _usersDTO;
            }
            set
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null
               && _httpContextAccessor.HttpContext.Session != null)
                {
                    _httpContextAccessor.HttpContext.Session.Set<usersDTO>(SessionConstants.USER_SESSION_OTP, value);
                }
            }
        }
    }
    public struct SessionConstants
    {
        public static string KEY_LOGIN = "Tata.Momerial.Web_Key_Login";
        public static string KEY_CHANGE_PASSWORD = "Tata.Momerial.Web_Key_Change_Password";
        public static string KEY_RESET_PASSWORD = "Tata.Momerial.Web_Key_Reset_Password";
        public static string USER_SESSION_COOKIE_NAME = "Tata.Momerial.Web_User_Session_COOKIE_NAME";

        public static string USER_SESSION = "Tata.Momerial.Web_User_Session";
        public static string USER_SESSION_OTP = "Tata.Momerial.Web_User_Session_OTP";
        public static string RESET_PASSWORD_USER_ID = "Tata.Momerial.Web_Reset_Password_UserID";
        public static string LANGUAGE_SESSION = "Tata.Momerial.Web_Language_Session";

        public static string CAPTCHA_KEY_LOGIN = "Tata.Momerial.Web_Key_Login_captcha";
        public static string CAPTCHA_KEY_CHANGE_PASSWORD = "Tata.Momerial.Web_Key_Change_Password_captcha";
        public static string CAPTCHA_KEY_RESET_PASSWORD = "Tata.Momerial.Web_Key_Reset_Password_captcha";
        public static string CAPTCHA_KEY_FORGOT_PASSWORD = "Tata.Momerial.Web_Key_Forgot_Password_captcha";

    }
}
