using Antlr.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using TMS.Data.Database;
using TMS.Model;

namespace TMS.Filter
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class TraceFilterAttribute : ActionFilterAttribute, IActionFilter
    {
      

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {


            string IPAdd = GetLocalIPAddress();
            var actionDescriptor = filterContext.ActionDescriptor;
            string controllerName = actionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = actionDescriptor.ActionName;
            string PageURL = HttpContext.Current.Request.Url.AbsoluteUri;
            int UpdatedBy = SessionHelper.UserId;
            var userAgent = HttpContext.Current.Request?.Browser.Browser.ToString();
            string userName = filterContext.HttpContext.User.Identity.Name.ToString();
            DateTime timeStamp = filterContext.HttpContext.Timestamp;
         
            ActivityLog message = new ActivityLog();
            message.ActionName = actionName;

            message.ControllerName = controllerName;
            message.CreatedOn = timeStamp;
            message.PageUrl = PageURL;
            message.UserId = UpdatedBy;
            message.IPAddress = IPAdd;
            message.BrowserName = userAgent;
  
            TMSEntities ctx = new TMSEntities();
            ctx.activityLogs.Add(message);
            ctx.SaveChanges();
            base.OnActionExecuted(filterContext);
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

    }
}

