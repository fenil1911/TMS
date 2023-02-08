using TMS.Data;
using TMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Service
{
    public class TicketService
    {
        private readonly TicketProvider ticketProvider;
        private readonly CommonLookupProvider commonLookupProvider;
        public TicketService()
        {
            ticketProvider = new TicketProvider();
            commonLookupProvider = new CommonLookupProvider();
        }
        public TicketModel GetTicketsById(int id)
        {
            var data = ticketProvider.GetTicketsById(id);
            TicketModel category = new TicketModel
            {
                Id = data.Id,
                AssignedTo=data.AssignedTo,
                Type = data.Type,
                DescriptionData=data.DescriptionData
                };
            return category;

        }
        public List<TicketModel> GetAllTickets()
        {
            var cammonlookup = ticketProvider.GetAllTickets();
            return GetAllTickets();
        }
        public int CreateTickets(TicketModel ticket)
        {
            return ticketProvider.CreateTickets(ticket);
        }
        public int CreateTicketStatus(TicketModel ticket)
        {
            return ticketProvider.CreateTicketStatus(ticket);
        }

        public List<CommonLookupModel> GetDropdownBykey(string key)
        {
            return commonLookupProvider.GetAllCommonLookup().Where(a => a.Type.ToLower() == key.ToLower()).ToList();
          
        }
        public List<CommonLookupModel> GetDropdownBykey1(string key)
        {
            return commonLookupProvider.GetAllCommonLookup().Where(a => a.Type.ToLower() == key.ToLower()).ToList();

        }

        public List<CommonLookupModel> GetDropdownBykey2(string key)
        {
            return commonLookupProvider.GetAllCommonLookup().Where(a => a.Type.ToLower() == key.ToLower()).ToList();

        }

    }
}
