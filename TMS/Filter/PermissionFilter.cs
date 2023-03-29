/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Controllers;
using TMS.Model;
using TMS.Service;

namespace TMS.Filter
{
    public class PermissionFilter : ActionFilterAttribute , BaseController
    {
      

        public override void OnActionExecuting(ActionExecutingContext filterContext) 
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.USER.ToString(), AccessPermission.IsView))
            {
                filterContext.Controller.ViewBag.ViewPermission = "tms";
            }
        }
    }

}
*/