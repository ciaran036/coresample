using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.Extensions;

namespace Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class EcomAuthorise : AuthorizeAttribute, IAuthorizationFilter
    {
        private string _controllerName;
        private string _actionName;
        private string _areaName;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Request.IsAjaxRequest()) return;
            _controllerName = context.RouteData.Values["Controller"].ToString().ToLower();
            _actionName = context.RouteData.Values["Action"].ToString().ToLower();
            _areaName = context.RouteData.DataTokens["area"]?.ToString().ToLower() ?? "";
            _areaName = context.RouteData.Values["Area"]?.ToString().ToLower() ?? "";

            // See here: https://stackoverflow.com/a/48228330/606602 
            // Add in Services, entities etc. 
        }
    }
}
