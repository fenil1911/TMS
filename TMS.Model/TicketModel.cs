using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Model
{
    public class TicketModel
    {
        public int Id { get; set; }
        [Required]
        public string TicketName { get; set; }
        [Display(Name = "Type")]
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string DescriptionData { get; set; }
        public int PriorityId { get; set; }
        public string PriorityName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string AssignedTo { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ImageName { get; set; }
        public List<MyDropdown> StatusDropdown { get; set; }
        public List<MyDropdown> PriorityDropdown { get; set; }
        public List<MyDropdown> TypeDropdown { get; set; }
        public DateTime UpdatedOn { get; set; }
        public TicketCommentViewModel TicketComment { get; set; }
    }
}