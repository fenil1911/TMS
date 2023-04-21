using TMS.Data;
using TMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Database;
using Kendo.Mvc;

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


            return data;

        }
        public TicketModel UpdateTicket(TicketModel model, int UpdatedBy)
        {
            return ticketProvider.UpdateTicket(model, UpdatedBy);
        }
        public IQueryable<TicketModel> GetAllTicketsAdmin(int pagesize, int page, IList<SortDescriptor> Sorts, string filters)
        {
            var commonlookup = ticketProvider.GetAllTicketsAdmin(pagesize, page, Sorts, filters).OrderByDescending(x => x.Id);
            return commonlookup;
        }
        public IQueryable<TicketModel> GetAllTickets(int pagesize, int page, IList<SortDescriptor> Sorts, string filters)
        {
            var commonlookup = ticketProvider.GetAllTickets(pagesize, page, Sorts, filters).OrderByDescending(x =>x.Id);
            return commonlookup;
        }
        public int CreateTickets(TicketModel ticket, int CreatedBy)
        {
            return ticketProvider.CreateTickets(ticket, CreatedBy);
        }
        public List<TicketModel> GetAllStatus()
        {
            var index = ticketProvider.GetAllStatus();
            return index;
        }   
        public List<TicketModel> GetAllPriority()
        {
            var index = ticketProvider.GetAllPriority();
            return index;
        }  
        public List<TicketModel> GetAllStatususer()
        {
            var index = ticketProvider.GetAllStatususer();
            return index;
        }
        public List<TicketModel> GetAllPriorityuser()
        {
            var index = ticketProvider.GetAllPriorityuser();
            return index;
        }     
        public int CreateTicketStatus(TicketStatus ticket, int CreatedBy)
        {
            return ticketProvider.CreateTicketStatus(ticket, CreatedBy);
        }
        public int CreateAttachment(TicketAttachment ticket, int CreatedBy)
        {
            return ticketProvider.CreateAttachment(ticket, CreatedBy);
        }
        public void EditAttachment(TicketAttachment ticket, int UpdatedBy)
        {
             ticketProvider.EditAttachment(ticket, UpdatedBy);
        }
        public List<MyDropdown> BindEmployee()
        {
            return ticketProvider.BindEmployee();
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
        public int CreateTicketComment(TicketCommentViewModel comment, int CreatedBy, string CreatedBy1)
        {
            return ticketProvider.CreateTicketComment(comment, CreatedBy, CreatedBy1);
        }                             
        public TicketCommentViewModel UpdatedTicketComment(TicketCommentViewModel comment, int UpdatedBy, string UpdatedBy1)
        {
            return ticketProvider.UpdatedTicketComment(comment, UpdatedBy, UpdatedBy1);
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
        public int TotalHigh()
        {
            return ticketProvider.TotalHigh();
        }
        public int TotalLow()
        {
            return ticketProvider.TotalLow();
        }
        public int TotalMedium()
        {
            return ticketProvider.TotalMedium();
        }
        public int TotalImmediate()
        {
            return ticketProvider.TotalImmediate();
        }
        public int TotalHighAdmin()
        {
            return ticketProvider.TotalHighAdmin();
        }
        public int TotalLowAdmin()
        {
            return ticketProvider.TotalLowAdmin();
        }
        public int TotalMediumAdmin()
        {
            return ticketProvider.TotalMediumAdmin();
        }
        public int TotalImmediateAdmin()
        {
            return ticketProvider.TotalImmediateAdmin();
        }
        public void CreateComment(TicketCommentViewModel model)
        {
            var comment = new TicketComment
            {
                TicketId = model.TicketId,
                Comment = model.Comment,
                CreatedBy = model.CreatedBy,
                CreatedOn = DateTime.Now,
                UserName = model.UserName
            };
            ticketProvider.CreateComment(comment);
        }
        public TicketCommentViewModel GetCommentById(int id)
        {
            var comment = ticketProvider.GetCommentById(id);
            if (comment != null)
            {
                var model = new TicketCommentViewModel
                {
                    Id = comment.Id,
                    TicketId = comment.TicketId,
                    Comment = comment.Comment,
                    CreatedBy = comment.CreatedBy,
                    CreatedOn = comment.CreatedOn,
                    UserName = comment.UserName
                };
                return model;
            }
            return null;
        }
        public void DeleteComment(int Id)
        {
            ticketProvider.DeleteComment(Id);
        }
    }
}
