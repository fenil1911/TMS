﻿using TMS.Data;
using TMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Database;

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
            data.StatusDropdown = GetDropdownBykey("Status")
               .Select(x => new MyDropdown() { id = x.Id, name = x.Name }).ToList();
            data.PriorityDropdown = GetDropdownBykey1("Priority")
               .Select(x => new MyDropdown() { id = x.Id, name = x.Name }).ToList();
            data.TypeDropdown = GetDropdownBykey2("Task")
                .Select(x => new MyDropdown() { id = x.Id, name = x.Name }).ToList();
            /* TicketModel category = new TicketModel
             {
                 TicketName=data.TicketName,
                 Id = data.Id,
                 AssignedTo = data.AssignedTo,
                 TypeId = data.TypeId,
                 DescriptionData = data.DescriptionData
             };*/
            return data;

        }
        public TicketModel UpdateTicket(TicketModel model, int UpdatedBy)
        {
            return ticketProvider.UpdateTicket(model,  UpdatedBy);
        }
        public List<TicketModel> GetAllTickets()
        {
            var commonlookup = ticketProvider.GetAllTickets();
            return commonlookup;    
        }
        public int CreateTickets(TicketModel ticket,int CreatedBy)
        {
            return ticketProvider.CreateTickets(ticket, CreatedBy);
        }
       

        public int CreateTicketStatus(TicketStatus ticket, int CreatedBy)
        {
            return ticketProvider.CreateTicketStatus(ticket ,CreatedBy);
        }
        public int CreateAttachment(TicketAttachment ticket, int CreatedBy)
        {
            return ticketProvider.CreateAttachment(ticket,CreatedBy);
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
        /*public int TicketComment(TicketComment ticket)
        {
            return ticketProvider.TicketComment(ticket);
        }*/
        public int CreateTicketComment(TicketCommentViewModel comment,int CreatedBy)
        {
            return ticketProvider.CreateTicketComment(comment, CreatedBy);
        }
        public void DeleteTicket(int Id)
        {
            ticketProvider.DeleteTicket(Id);
        }
        public List<TicketCommentViewModel> GetAllComment(int Id)
        {
            var GetAllComment = ticketProvider.GetAllComment(Id);
            return GetAllComment;
        }
    }
}
