﻿using System;
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

        public TicketModel GetTicketsById(int Id)
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
                              where t.Id == Id
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
                                  ImageName = ta.Filename,
                                  StatusId = c.Id,
                                  PriorityId = c1.Id,
                                  TypeId = c2.Id,

                              }).FirstOrDefault();
            return alltickets;
        }
        public Tickets GetTicketsByUpdateId(int Id)
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
                              where t.IsDeleted != 1
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
        public int CreateTicketComment(TicketCommentViewModel model, int CreatedBy)
        {
            TicketComment obj = new TicketComment()
            {
                Comment = model.Comment,

                TicketId = model.TicketId,
                CreatedBy = CreatedBy,
                CreatedOn = DateTime.Now
            };
            _db.TicketComment.Add(obj);
            _db.SaveChanges();
            return obj.Id;
        }
        public int CreateTickets(TicketModel ticket, int CreatedBy)
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
                CreatedOn = DateTime.Now,
                CreatedBy = CreatedBy
            };
            _db.Tickets.Add(_ticket);
            _db.SaveChanges();
            return _ticket.Id;
        }


        public TicketModel UpdateTicket(TicketModel model, int UpdatedBy)
        {
            var obj = GetTicketsByUpdateId(model.Id);
            obj.AssignedTo = model.AssignedTo;
            obj.DescriptionData = model.DescriptionData;
            obj.TicketName = model.TicketName;
            obj.StatusId = model.StatusId;
            obj.PriorityId = model.PriorityId;
            obj.TypeId = model.TypeId;
            obj.UpdatedOn = DateTime.Now;
            obj.UpdatedBy = UpdatedBy;
            _db.SaveChanges();
            return model;

        }
        public int CreateTicketStatus(TicketStatus model, int CreatedBy)
        {
            CreatedBy = model.CreatedBy;
            _db.TicketStatus.Add(model);
            _db.SaveChanges();
            return model.Id;
        }

        public int CreateAttachment(TicketAttachment model1, int CreatedBy)
        {
            TicketAttachment _ticket = new TicketAttachment()
            {
                CreatedBy = model1.CreatedBy,

            };

            _db.TicketAttachment.Add(model1);
            _db.SaveChanges();
            return model1.Id;
        }
        public void DeleteTicket(int Id)
        {
            var data = GetTicketsByUpdateId(Id);
            if (data != null)
            {

                Tickets model = _db.Tickets.Find(Id);
                model.IsDeleted = 1;

                _db.SaveChanges();
            }
        }
        public List<TicketCommentViewModel> GetAllComment(int Id)
        {
            var GetAllComment = (from t in _db.TicketComment
                                 where t.TicketId == Id
                                 select new TicketCommentViewModel
                                 {
                                     Comment = t.Comment,
                                     CreatedOn = t.CreatedOn,
                                     CreatedBy = t.CreatedBy
                                 })
                                 .OrderByDescending(x => x.CreatedOn)
                                 .ToList();
            return GetAllComment;
        }
    }

}
