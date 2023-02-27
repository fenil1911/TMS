using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TMS.Model
{
    public class EmailModel
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string UserName { get; set; }
        
        

        public HttpPostedFileBase AttachmentFile { get; set; }
    }
}
