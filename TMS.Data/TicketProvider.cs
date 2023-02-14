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
           

            var alltickets = (from t in _db.Tickets
                              join c in _db.CommonLookup on
                              t.StatusId equals c.Id
                              join c1 in _db.CommonLookup on
                              t.PriorityId equals c1.Id
                              join c2 in _db.CommonLookup on
                              t.TypeId equals c2.Id
                              join a in _db.TicketAttachment
                              on t.Id equals a.TicketId into a
                              from ta in a.DefaultIfEmpty()
                              select new TicketModel
                              {
                                  Id = t.Id,
                                  TicketName = t.TicketName,
                                  AssignedTo = t.AssignedTo,
                                  TypeName = c2.Name,
                                  DescriptionData = t.DescriptionData,
                                  StatusName = c.Name,
                                  PriorityName = c1.Name,
                                  CreatedOn = (DateTime)t.CreatedOn,
                                  ImageName = ta.Filename
                              }).ToList();
            return alltickets;            
        }
        public int CreateTickets(TicketModel ticket)
        {

            Tickets _ticket = new Tickets()
            {
                //Id = ticket.Id,
                AssignedTo = ticket.AssignedTo,
                DescriptionData = ticket.DescriptionData,
                TicketName = ticket.TicketName,
                StatusId = ticket.StatusId,
                PriorityId = ticket.PriorityId,
                TypeId = ticket.TypeId,
                CreatedOn = DateTime.Now


            };
            _db.Tickets.Add(_ticket);
            _db.SaveChanges();
            return _ticket.Id;
        }
        /*public int TicketComment(TicketModel model)
        {

            TicketComment _comment = new TicketComment()
            {
                

            };
            _db.TicketComment.Add(_comment);
            _db.SaveChanges();
            return model.Id;
        }*/
        public TicketModel UpdateTicket(TicketModel model)
        {
            var obj = GetTicketsById(model.Id);
            obj.AssignedTo = model.AssignedTo;
            obj.DescriptionData = model.DescriptionData;
            obj.TicketName = model.TicketName;
            obj.StatusId = model.StatusId;
            obj.PriorityId = model.PriorityId;
            obj.TypeId = model.TypeId;
            obj.UpdatedOn = DateTime.Now;

            _db.SaveChanges();
            return model;

        }
        public int CreateTicketStatus(TicketStatus model)
        {
            _db.TicketStatus.Add(model);
            _db.SaveChanges();
            return model.Id;
        }
        public int CreateAttachment(TicketAttachment model1)
        {
            TicketAttachment _ticket = new TicketAttachment()
            {

            };
            _db.TicketAttachment.Add(model1);
            _db.SaveChanges();
            return model1.Id;
        }
    }

}
