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
            var all = (from t in _db.Tickets
                       select t);

            var alltickets = (from t in _db.Tickets
                              join c in _db.CommonLookup on
                              t.StatusId equals c.Id
                              join c1 in _db.CommonLookup on
                              t.PriorityId equals c1.Id
                              join c2 in _db.CommonLookup on
                              t.TypeId equals c2.Id

                              select new TicketModel
                              {
                                  Id = t.Id,
                                  TicketName = t.TicketName,
                                  AssignedTo = t.AssignedTo,
                                  TypeName = c2.Name,
                                  DescriptionData = t.DescriptionData,
                                  StatusName = c.Name,
                                  PriorityName = c1.Name,
                                  CreatedOn = DateTime.Now



                              }).ToList();
            return alltickets;
            /*    return _db.Tickets.Select(x => new TicketModel
                {
                    Id = x.Id,
                    Type = x.Type,
                    AssignedTo = x.AssignedTo,
                    DescriptionData = x.DescriptionData,
                    StatusId = statusStr

                }).ToList();*/

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
                PriorityId = ticket.PriorityId,
                TypeId = ticket.TypeId,
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
