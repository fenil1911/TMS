using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data.Database
{
    [Table("TicketAttachment")]
    public class TicketAttachment
    {
        [Key]
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Filename { get; set; }
        public String CreatedBy{ get; set; }
        public DateTime CreatedOn { get; set; }



    }
}
