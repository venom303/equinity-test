using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Interview
{
    public class TokenAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (HttpContext.Current.User == null && actionContext.Request.Headers.Authorization?.Parameter == "secrettoken")
            {
                HttpContext.Current.User = new GenericPrincipal(new GenericIdentity("user"), null);
                return;
            }

            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }
    }
}