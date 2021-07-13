using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_Project.Filters
{
    public class CustomFilter:ActionFilterAttribute
    {
        //
        // Summary:
        //     Called by the ASP.NET MVC framework after the action method executes.
        //
        // Parameters:
        //   filterContext:
        //     The filter context.
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
          
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            (filterContext.Result as ViewResult).ViewBag.UploadMsg = "Upload is completed";
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

        }
    }
}