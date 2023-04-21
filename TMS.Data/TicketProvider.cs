using Kendo.Mvc;
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

        public Tickets GetTicketsByUpdateId(int Id)
        {
            return _db.Tickets.Find(Id);
        }

        public TicketComment GetTicketsCommentUpdated(int Id)
        {
            return _db.TicketComment.Find(Id);
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

                              join c4 in _db.Users on
                              t.CreatedBy equals c4.UserId
                              join c5 in _db.Users on
                              t.AssignedTo equals c5.UserId

                              join a in _db.TicketAttachment
                              on t.Id equals a.TicketId into a
                              from ta in a.DefaultIfEmpty()
                              where t.Id == Id
                              select new TicketModel
                              {
                                  Id = t.Id,
                                  TicketName = t.TicketName,
                                  AssignedTo = t.AssignedTo,
                                  AssignedToName = c5.UserName,
                                  CreatedToName = c4.UserName,
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

        public IQueryable<TicketModel> GetAllTickets(int pagesize, int page, IList<SortDescriptor> Sorts, string filters)
        {
            var GetCount = _db.Tickets.Count(X => X.IsDeleted != 1);
            if (Sorts.Any())
            {
                var allTicketsUsers = (from t in _db.Tickets
                                       join c in _db.CommonLookup on
                                       t.StatusId equals c.Id
                                       join c1 in _db.CommonLookup on
                                       t.PriorityId equals c1.Id
                                       join c2 in _db.CommonLookup on
                                       t.TypeId equals c2.Id
                                       join c3 in _db.Users on
                                       t.AssignedTo equals c3.UserId
                                       join c4 in _db.Users on
                                       t.CreatedBy equals c4.UserId
                                       join a in _db.TicketAttachment
                                       on t.Id equals a.TicketId into a
                                       from ta in a.DefaultIfEmpty()
                                       where t.AssignedTo == SessionHelper.UserId &&
                                       t.IsDeleted != 1 && (
                                      filters == null ||
                                      t.TicketName.Contains(filters) ||
                                      t.DescriptionData.Contains(filters) ||
                                      c1.Name.Contains(filters) ||
                                      c.Name.Contains(filters) ||
                                      c3.UserName.Contains(filters) ||
                                      c2.Name.Contains(filters)
                                        )
                                       select new TicketModel
                                       {
                                           Id = t.Id,
                                           TicketName = t.TicketName,
                                           AssignedToName = c3.UserName,
                                           CreatedToName = c4.UserName,
                                           TypeName = c2.Name,
                                           DescriptionData = t.DescriptionData,
                                           StatusName = c.Name,
                                           PriorityName = c1.Name,
                                           CreatedOn = (DateTime)t.CreatedOn,
                                           ImageName = ta.Filename,
                                           count = GetCount
                                       }).OrderByDescending(x => x.CreatedOn).AsQueryable();

                return allTicketsUsers;
            }
            else
            {
                var allTicketsUsers = (from t in _db.Tickets
                                       join c in _db.CommonLookup on
                                       t.StatusId equals c.Id
                                       join c1 in _db.CommonLookup on
                                       t.PriorityId equals c1.Id
                                       join c2 in _db.CommonLookup on
                                       t.TypeId equals c2.Id
                                       join c3 in _db.Users on
                                       t.AssignedTo equals c3.UserId
                                       join c4 in _db.Users on
                                       t.CreatedBy equals c4.UserId
                                       join a in _db.TicketAttachment
                                       on t.Id equals a.TicketId into a
                                       from ta in a.DefaultIfEmpty()
                                       where t.AssignedTo == SessionHelper.UserId &&
                                       t.IsDeleted != 1 && (
                                        filters == null ||
                                        t.TicketName.Contains(filters) ||
                                        t.DescriptionData.Contains(filters) ||
                                        c1.Name.Contains(filters) ||
                                        c.Name.Contains(filters) ||
                                        c3.UserName.Contains(filters) ||
                                        c2.Name.Contains(filters)
                                         )

                                       select new TicketModel
                                       {
                                           Id = t.Id,
                                           TicketName = t.TicketName,
                                           AssignedToName = c3.UserName,
                                           CreatedToName = c4.UserName,
                                           TypeName = c2.Name,
                                           DescriptionData = t.DescriptionData,
                                           StatusName = c.Name,
                                           PriorityName = c1.Name,
                                           CreatedOn = (DateTime)t.CreatedOn,
                                           ImageName = ta.Filename,
                                           count = GetCount
                                       }).OrderByDescending(x => x.CreatedOn)
                                      .Skip((page - 1) * pagesize)
                                      .Take(pagesize).AsQueryable();

                return allTicketsUsers;

            }
        }

        public IQueryable<TicketModel> GetAllTicketsAdmin(int pagesize, int page, IList<SortDescriptor> Sorts, string filters)
        {
            var GetCount = _db.Tickets.Count(X => X.IsDeleted != 1);
            if (Sorts.Any())
            {
                var allticketsadmin = (from t in _db.Tickets
                                       join c in _db.CommonLookup on
                                       t.StatusId equals c.Id
                                       join c1 in _db.CommonLookup on
                                       t.PriorityId equals c1.Id
                                       join c2 in _db.CommonLookup on
                                       t.TypeId equals c2.Id
                                       join c3 in _db.Users on
                                       t.AssignedTo equals c3.UserId
                                       join c4 in _db.Users on
                                       t.CreatedBy equals c4.UserId
                                       join a in _db.TicketAttachment
                                       on t.Id equals a.TicketId into a
                                       from ta in a.DefaultIfEmpty()
                                       where t.IsDeleted != 1 && (
                                      filters == null ||
                                      t.TicketName.Contains(filters) ||
                                      t.DescriptionData.Contains(filters) ||
                                      c1.Name.Contains(filters) ||
                                      c.Name.Contains(filters) ||
                                      c3.UserName.Contains(filters) ||
                                      c2.Name.Contains(filters)
                                )
                                       select new TicketModel
                                       {
                                           Id = t.Id,
                                           TicketName = t.TicketName,
                                           AssignedToName = c3.UserName,
                                           CreatedToName = c4.UserName,
                                           TypeName = c2.Name,
                                           DescriptionData = t.DescriptionData,
                                           StatusName = c.Name,
                                           PriorityName = c1.Name,
                                           CreatedOn = (DateTime)t.CreatedOn,
                                           ImageName = ta.Filename,
                                           count = GetCount
                                       }).OrderByDescending(x => x.CreatedOn).AsQueryable();

                return allticketsadmin;
            }
            else
            {
                var allticketsadmin = (from t in _db.Tickets
                                       join c in _db.CommonLookup on
                                       t.StatusId equals c.Id
                                       join c1 in _db.CommonLookup on
                                       t.PriorityId equals c1.Id
                                       join c2 in _db.CommonLookup on
                                       t.TypeId equals c2.Id
                                       join c3 in _db.Users on
                                       t.AssignedTo equals c3.UserId
                                       join c4 in _db.Users on
                                       t.CreatedBy equals c4.UserId
                                       join a in _db.TicketAttachment
                                       on t.Id equals a.TicketId into a
                                       from ta in a.DefaultIfEmpty()
                                       where t.IsDeleted != 1 && (
                                        filters == null ||
                                        t.TicketName.Contains(filters) ||
                                        t.DescriptionData.Contains(filters) ||
                                        c1.Name.Contains(filters) ||
                                        c.Name.Contains(filters) ||
                                        c3.UserName.Contains(filters) ||
                                        c2.Name.Contains(filters)
                                  )

                                       select new TicketModel
                                       {
                                           Id = t.Id,
                                           TicketName = t.TicketName,
                                           AssignedToName = c3.UserName,
                                           CreatedToName = c4.UserName,
                                           TypeName = c2.Name,
                                           DescriptionData = t.DescriptionData,
                                           StatusName = c.Name,
                                           PriorityName = c1.Name,
                                           CreatedOn = (DateTime)t.CreatedOn,
                                           ImageName = ta.Filename,
                                           count = GetCount
                                       }).OrderByDescending(x => x.CreatedOn)
                                      .Skip((page - 1) * pagesize)
                                      .Take(pagesize).AsQueryable();

                return allticketsadmin;

            }
        }

        public int CreateTicketComment(TicketCommentViewModel model, int CreatedBy, string CreatedBy1)
        {
            TicketComment obj = new TicketComment()
            {
                Comment = model.Comment,

                TicketId = model.TicketId,
                CreatedBy = CreatedBy,
                UserName = CreatedBy1,

                CreatedOn = DateTime.Now
            };
            _db.TicketComment.Add(obj);
            _db.SaveChanges();
            return obj.Id;
        }

        public TicketCommentViewModel UpdatedTicketComment(TicketCommentViewModel model, int UpdatedBy, string UpdatedBy1)
        {
            var obj = GetTicketsCommentUpdated(model.Id);
            obj.Comment = model.Comment;
            obj.Id = model.Id;

            obj.UpdatedBy = UpdatedBy;
            obj.UserName = UpdatedBy1;
            obj.UpdatedOn = DateTime.Now;

            _db.SaveChanges();
            return model;
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

        public List<MyDropdown> BindEmployee()
        {
            return _db.Users.Where(s => s.IsActive == true).Select(x => new MyDropdown { Key = x.FirstName, id = x.UserId }).ToList();
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

        public void EditAttachment(TicketAttachment model, int UpdatedBy)
        {
            var attachmentId = (from fa in _db.TicketAttachment
                                where fa.TicketId == model.TicketId
                                select fa.Id).FirstOrDefault();

            if (attachmentId == 0)
            {
                TicketAttachment _ticket = new TicketAttachment()
                {
                    UpdatedBy = model.UpdatedBy,

                };

                _db.TicketAttachment.Add(model);
                _db.SaveChanges();

            }
            else
            {
                TicketAttachment attachment = _db.TicketAttachment.Find(attachmentId);


                attachment.Filename = model.Filename;

                attachment.UpdatedBy = UpdatedBy;
                attachment.UpdatedOn = DateTime.Now;

                _db.SaveChanges();
            }

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
                                 where t.TicketId == Id && t.IsDeleted != true
                                 select new TicketCommentViewModel
                                 {
                                     Id = t.Id,
                                     Comment = t.Comment,
                                     CreatedOn = t.CreatedOn,
                                     CreatedBy = t.CreatedBy,
                                     UserName = t.UserName
                                 }).OrderByDescending(x => x.CreatedOn)

                                 .ToList();
            return GetAllComment;
        }

        public int TotalHigh()
        {
            var high = _db.Tickets.Where(x => x.PriorityId == 11 && x.IsDeleted != 1 && x.AssignedTo == SessionHelper.UserId).Count();
            return high;
        }

        public int TotalLow()
        {
            return _db.Tickets.Where(x => x.PriorityId == 12 && x.IsDeleted != 1 && x.AssignedTo == SessionHelper.UserId).Count();

        }

        public int TotalMedium()
        {
            return _db.Tickets.Where(x => x.PriorityId == 13 && x.IsDeleted != 1 && x.AssignedTo == SessionHelper.UserId).Count();

        }

        public int TotalImmediate()
        {
            return _db.Tickets.Where(x => x.PriorityId == 14 && x.IsDeleted != 1 && x.AssignedTo == SessionHelper.UserId).Count();

        }        

        public int TotalHighAdmin()
        {
            var high = _db.Tickets.Where(x => x.PriorityId == 11 && x.IsDeleted != 1).Count();
            return high;
        }

        public int TotalLowAdmin()
        {
            return _db.Tickets.Where(x => x.PriorityId == 12 && x.IsDeleted != 1).Count();

        }

        public int TotalMediumAdmin()
        {
            return _db.Tickets.Where(x => x.PriorityId == 13 && x.IsDeleted != 1).Count();

        }

        public int TotalImmediateAdmin()
        {
            return _db.Tickets.Where(x => x.PriorityId == 14 && x.IsDeleted != 1).Count();

        }

        public List<TicketModel> GetAllStatus()
        {
            var statusCounts = (from t in _db.Tickets
                                join s in _db.CommonLookup
                                on t.StatusId equals s.Id
                                where t.IsDeleted != 1
                                group s by s.Name into g
                                select new TicketModel { StatusName = g.Key, count = g.Count() }).ToList();
            return statusCounts;
        } 
        public List<TicketModel> GetAllPriority()
        {
            var statusCounts = (from t in _db.Tickets
                                join s in _db.CommonLookup
                                on t.PriorityId equals s.Id
                                where t.IsDeleted != 1
                                group s by s.Name into g
                                select new TicketModel { PriorityName = g.Key, count = g.Count() }).ToList();
            return statusCounts;
        }

        public List<TicketModel> GetAllStatususer()
        {
            var statusCounts = (from t in _db.Tickets
                                join s in _db.CommonLookup
                                on t.StatusId equals s.Id
                                where (t.AssignedTo == SessionHelper.UserId && t.IsDeleted != 1)
                                group s by s.Name into g
                                select new TicketModel { StatusName = g.Key, count = g.Count() }).ToList();
            return statusCounts;
        }
        public List<TicketModel> GetAllPriorityuser()
        {
            var statusCounts = (from t in _db.Tickets
                                join s in _db.CommonLookup
                                on t.PriorityId equals s.Id
                                where (t.AssignedTo == SessionHelper.UserId && t.IsDeleted != 1)
                                group s by s.Name into g
                                select new TicketModel { PriorityName = g.Key, count = g.Count() }).ToList();
            return statusCounts;
        }

        public void CreateComment(TicketComment comment)
        {
            _db.TicketComment.Add(comment);
            _db.SaveChanges();
        }

        public TicketComment GetCommentById(int id)
        {
            return _db.TicketComment.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateComment(TicketCommentViewModel comment, int UpdatedBy, string UpdatedBy1)
        {
            var existingComment = _db.TicketComment.FirstOrDefault(x => x.Id == comment.Id);
            if (existingComment != null)
            {
                existingComment.Comment = comment.Comment;
                existingComment.CreatedBy = comment.CreatedBy;
                existingComment.CreatedOn = comment.CreatedOn;
                existingComment.UserName = comment.UserName;
                existingComment.UpdatedBy = UpdatedBy;
                _db.SaveChanges();
            }
        }

        public void DeleteComment(int Id)
        {
            var data = GetAllComment(Id);
            if (data != null)
            {

                TicketComment model = _db.TicketComment.Find(Id);
                model.IsDeleted = true;

                _db.SaveChanges();
            }
        }
    }

}
