using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data.Database
{
    [Table("TicketComment")]
    public class TicketComment
    {
        [Key]
        public int Id { get; set; }
        public string TicketId { get; set; }
        public string Comment { get; set; }
        public string CreatedBy { get; set; }

    }
}
