using System.Web;
using System.Web.Mvc;
using TMS.Filter;

namespace TMS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AuthorizeAttribute());
           /* filters.Add(new HandleErrorAttribute());*/
           // filters.Add(new ExceptionHandlerAttribute());
            filters.Add(new TraceFilterAttribute());

        }
    }
}
