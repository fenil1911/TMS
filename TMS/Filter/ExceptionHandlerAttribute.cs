/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Data.Database;
using TMS.Model;
using System.Diagnostics;

namespace TMS.Filter
{
    public class ExceptionHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                string ControllerName = filterContext.RouteData.Values["controller"].ToString();
                string ActionName = filterContext.RouteData.Values["Action"].ToString();
                int UpdatedBy = SessionHelper.UserId;
                string PageURL = HttpContext.Current.Request.Url.AbsoluteUri;

                ErrorLog_Mst logger = new ErrorLog_Mst();
                logger.Message = filterContext.Exception.StackTrace;
                logger.ActionName = ActionName;
                logger.ControllerName = ControllerName;
                logger.PageURL = PageURL;
                logger.UpdatedOn = DateTime.Now;
                logger.UpdatedBy = UpdatedBy;


                TMSEntities ctx = new TMSEntities();

                ctx.ErrorLog_Msts.Add(logger);
                ctx.SaveChanges();

                filterContext.ExceptionHandled = true;
            }
        }
    }
}
*/