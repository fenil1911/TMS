using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Model
{
    public class TicketCommentViewModel
    {
        public int Id { get; set; }
        public string TicketId { get; set; }
        public string Comment { get; set; }
        public string CreatedBy { get; set; }

    }
}
