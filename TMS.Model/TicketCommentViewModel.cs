using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Model
{
    public class TicketCommentViewModel
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        [Required(ErrorMessage = "Comment is required")]
        
        public string Comment { get; set; }
        public string UserName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
