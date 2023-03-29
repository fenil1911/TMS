using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Model
{
    public class FilterModel
    {

        public class ActivityLog
        {
            public int Id { get; set; }

            public string PageUrl { get; set; }
            public string IPAddress { get; set; }
            public string ActionName { get; set; }
            public string ControllerName { get; set; }
            public string UserName { get; set; }
            public string BrowserName { get; set; }
            public int UserId { get; set; }
            public DateTime CreatedOn { get; set; }
            public int count { get; set; }
            /*public DateTime Duration{ get; set; }*/



        }


        public class ErrorLog
        {
          
            public int Id { get; set; }
            public string ControllerName { get; set; }
            public string Message { get; set; }
            public string ActionName { get; set; }
            public string PageURL { get; set; }
            public string UserName { get; set; }
            public int UpdatedBy { get; set; }
            public DateTime UpdatedOn { get; set; }
            public int count { get; set; }
        }
    }
}
