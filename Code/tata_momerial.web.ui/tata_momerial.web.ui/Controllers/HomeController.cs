using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using tata_momerial.web.common.Cookie;
using tata_momerial.web.ui.Business;
using IMailService = tata_momerial.web.common.IMailService;


namespace tata_momerial.web.ui.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ICookieService objCookieService, ICompositeViewEngine viewEngine,
         ILogger<BaseController> logger, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IMailService _mailService) : base(objCookieService,
             viewEngine, env, _mailService, logger)
        {

        }

        [AllowAnonymous]
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            try
            {
                return RedirectToAction(WebConstants.ActionViewNames.ACTION_LOGIN,
                    WebConstants.ActionViewNames.HOME_CONTROLLER_NAME);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("error")]
        public IActionResult Error()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("resource-not-found")]
        public IActionResult ResourceNotFound()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("unauthorized-access")]
        public IActionResult UnauthorizedAccess()
        {
            return View();
        }

    }
}
