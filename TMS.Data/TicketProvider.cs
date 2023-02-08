using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Database;
using TMS.Model;

namespace TMS.Data
{
    public class TicketProvider : BaseProvider
    {


        public TicketProvider()
        {
        }
        public Tickets GetTicketsById(int Id)
        {
            return _db.Tickets.Find(Id);
        }
        public List<TicketModel> GetAllTickets()
        {



            return GetAllTickets();

        }
        public int CreateTickets(TicketModel ticket)
        {

            Tickets _ticket = new Tickets()
            {
                Id = ticket.Id,
                AssignedTo = ticket.AssignedTo,
                DescriptionData = ticket.DescriptionData,
                TicketName = ticket.TicketName,
                StatusId = ticket.StatusId,
                PriorityID = ticket.PriorityID,
                Type = ticket.Type,
                CreatedOn = DateTime.Now


            };
            _db.Tickets.Add(_ticket);
            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ticket.Id;
        }
        public int CreateTicketStatus(TicketModel ticket)
        {
            //var statusStr = _db.CommonLookup.Where(x => x.Id == ticket.StatusId).Select(x => x.Name).ToString();
            var statusStr = (from c in _db.CommonLookup
                             where c.Id == ticket.StatusId
                             select c.Name).FirstOrDefault();
            TicketStatus ticketStatus = new TicketStatus()
            {


                TicketId = ticket.Id,
                NewStatus = statusStr

            };

            _db.TicketStatus.Add(ticketStatus);
            _db.SaveChanges();
            return ticketStatus.Id;

        }
        
    }

}
