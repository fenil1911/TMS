using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data.Database
{
    [Table("ActivityLog")]
    public class ActivityLog
    {
        [Key]
        public int Id { get; set; }
      
        public string PageUrl { get; set; }
        public string IPAddress { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string BrowserName { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Duration { get; set; }


    }
}
