using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text;
using tata_momerial.web.common;
using tata_momerial.web.common.Cookie;
using tata_momerial.web.ui.Business;


namespace tata_momerial.web.ui.Controllers
{
    public class BaseController : Controller
    {
        private readonly ILogger<BaseController> _logger;

        internal readonly IWebHostEnvironment HostingEnvrionment;
        protected ICompositeViewEngine viewEngine;

        internal readonly ICookieService CookieService;
        internal readonly IMailService mailService;

        internal readonly SessionManager SessionManager;

        public ApplicationConfiguration ApplicationConfiguration => AppServicesHelper.ApplicationConfiguration;
        public BaseController(ICookieService objCookieService, ICompositeViewEngine viewEngine, IWebHostEnvironment env, IMailService _mailService, ILogger<BaseController> logger)
        {
            this.viewEngine = viewEngine;
            HostingEnvrionment = env;
            CookieService = objCookieService;
            mailService = _mailService;
            SessionManager = new SessionManager(objCookieService);
            _logger = logger;
        }

        public void LogException(Exception ex)
        {
            try
            {
                if (ex != null)
                {
                    _logger.LogError(ex.Message + ex.StackTrace + ex.InnerException);
                }
            }
            catch (Exception logEx)
            {
                // Handle logging failure, e.g., write to a file or database
                Console.WriteLine("Logging failed: " + logEx.Message);
            }
        }

        protected string ReturnFailedMessageString(ModelStateDictionary ModelState)
        {
            StringBuilder sb = new StringBuilder();
            var validationErrors = ModelState.Values.Select(x => x.Errors);
            validationErrors.ToList().ForEach(ve =>
            {
                var errorStrings = ve.Select(x => x.ErrorMessage);
                errorStrings.ToList().ForEach(em =>
                {
                    sb.AppendLine(em + ",");
                });
            });
            return sb.ToString();

        }
        protected string RenderViewAsString(object model, string? viewName = null)
        {
            viewName = viewName ?? ControllerContext.ActionDescriptor.ActionName;
            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                IView? view = viewEngine.FindView(ControllerContext, viewName, true).View;
                if (view == null)
                    return string.Empty;

                ViewContext viewContext = new ViewContext(ControllerContext, view, ViewData, TempData, sw, new HtmlHelperOptions());

                view.RenderAsync(viewContext).Wait();

                return sw.GetStringBuilder().ToString();
            }
        }
        protected string RenderViewAsString(string? viewName = null)
        {
            viewName = viewName ?? ControllerContext.ActionDescriptor.ActionName;
            ViewData.Model = null;

            using (StringWriter sw = new StringWriter())
            {
                IView? view = viewEngine.FindView(ControllerContext, viewName, true).View;
                if (view == null)
                    return string.Empty;

                ViewContext viewContext = new ViewContext(ControllerContext, view, ViewData, TempData, sw, new HtmlHelperOptions());

                view.RenderAsync(viewContext).Wait();

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
