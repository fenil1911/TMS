using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Model
{
    public class TicketListModel
    {
        public int Id { get; set; }
        public string TicketName { get; set; }
        public string Type { get; set; }
        public string DescriptionData { get; set; }
        public int PriorityName { get; set; }
        public int StatusName { get; set; }
        public string AssignedTo { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<MyDropdown> StatusDropdown { get; set; }
        public List<MyDropdown> PriorityDropdown { get; set; }
        public List<MyDropdown> TypeDropdown { get; set; }
    }
}
