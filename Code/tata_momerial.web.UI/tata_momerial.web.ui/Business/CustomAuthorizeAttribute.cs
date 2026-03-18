using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using tata_momerial.web.dto;

namespace tata_momerial.web.ui.Business
{
    public class CustomAuthorizeAttribute : ActionFilterAttribute
    {
        public string authorizationtypeid { get; set; } = string.Empty;
        public bool isview { get; set; }
        public bool isadd { get; set; }
        public bool isupdate { get; set; }
        public bool isdelete { get; set; }
        public bool isjsonresponse { get; set; }
        public bool isloginrequired { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            bool blnAccessView = false;
            bool blnAccessAdd = false;
            bool blnAccessUpdate = false;
            bool blnAccessDelete = false;
            string loginPage = "~/login";
            string unauthorizedAccess = "~/UnauthorizedAccess";


            usersDTO usersDTO = context.HttpContext.Session.Get<usersDTO>(SessionConstants.USER_SESSION);

            if (isloginrequired && usersDTO == null)
            {
                context.Result = new RedirectResult(loginPage);
            }
            else if (isloginrequired && usersDTO != null)
            {
            }
            else
            {
                if (usersDTO != null)
                {
                    roleauthorizationtyperelationshipDTO objRoleAuthorizationTypeRelationshipDTO = null;

                    if (usersDTO.lstroleauthorizationtyperelationshipDTO != null)
                    {
                        objRoleAuthorizationTypeRelationshipDTO = usersDTO.lstroleauthorizationtyperelationshipDTO.FirstOrDefault(x => x.authorizationtypeid.ToString() == authorizationtypeid);

                        if (objRoleAuthorizationTypeRelationshipDTO != null)
                        {
                            if (isdelete
                                && objRoleAuthorizationTypeRelationshipDTO.isdelete == 1
                                && objRoleAuthorizationTypeRelationshipDTO.isview == 1)
                            {
                                blnAccessDelete = true;
                            }
                            else if (!isdelete)
                            {
                                blnAccessDelete = true;
                            }

                            if (isupdate
                                && objRoleAuthorizationTypeRelationshipDTO.isupdate == 1
                                && objRoleAuthorizationTypeRelationshipDTO.isview == 1)
                            {
                                blnAccessUpdate = true;
                            }
                            else if (!isupdate)
                            {
                                blnAccessUpdate = true;
                            }

                            if (isadd
                               && objRoleAuthorizationTypeRelationshipDTO.isinsert == 1
                               && objRoleAuthorizationTypeRelationshipDTO.isview == 1)
                            {
                                blnAccessAdd = true;
                            }
                            else if (!isadd)
                            {
                                blnAccessAdd = true;
                            }

                            if (isview
                               && objRoleAuthorizationTypeRelationshipDTO.isview == 1)
                            {
                                blnAccessView = true;
                            }
                            else if (!isview)
                            {
                                blnAccessView = true;
                            }
                        }
                    }

                    if (blnAccessView && blnAccessAdd && blnAccessDelete && blnAccessUpdate)
                    {

                    }
                    else
                    {
                        if (!isjsonresponse)
                        {
                            context.Result = new RedirectResult(unauthorizedAccess);
                        }
                        else
                        {
                            context.Result = new RedirectResult(unauthorizedAccess);
                        }
                    }
                }
                else if (!isjsonresponse)
                {
                    context.Result = new RedirectResult(loginPage);
                }
                else
                {
                    context.Result = new RedirectResult(loginPage);
                }
            }

        }
    }
    public class ValidateHeaderAntiForgeryTokenAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var httpContext = context.HttpContext;
            bool isValidToken = false;
            if (httpContext != null && httpContext.Session != null)
            {
                var requestToken = httpContext.Request.Form["RequestVerificationToken"];

                // validate Token from Session
                string sessionToken = httpContext.Session.GetString(requestToken);
                if (!string.IsNullOrWhiteSpace(sessionToken))
                {
                    isValidToken = true;
                    httpContext.Session.Remove(sessionToken);
                }
            }

            if (!isValidToken)
                context.Result = new RedirectResult("~/Error");

        }
    }
}
