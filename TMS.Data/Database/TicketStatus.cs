using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data.Database
{
    public class TicketStatus
    {
        [Key]
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
    }
}
