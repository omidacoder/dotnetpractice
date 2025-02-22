using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.Common.Filters
{
    public class UserCheckIdFilter : ActionFilterAttribute
    {


        //public override void OnActionExecuting(ActionExecutingContext context)
        //{

        //    if (context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value != context.RouteData.Values["userId"].ToString())
        //    {
        //        context.Result = new UnauthorizedResult();
        //    }

        //    base.OnActionExecuting(context);
        //}
    }
}
