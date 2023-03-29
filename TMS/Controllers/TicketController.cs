using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using TMS.Model;
using TMS.Service;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using TMS.Data.Database;
using TMS.Helper;
using Kendo.Mvc;
using System.ComponentModel;

namespace TMS.Controllers
{
    public class TicketController : BaseController
    {
        private readonly TicketService _ticketService;
        private readonly CommonLookupService _commonLookupService;
        private readonly UsersService _usersService;
        public TMSEntities _db;
       

        public TicketController()
        {
            _ticketService = new TicketService();
            _commonLookupService = new CommonLookupService();
            _usersService = new UsersService();
            _db = new TMSEntities();

        }

        public ActionResult Index([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                ViewBag.ViewPermission = "TICKET";
                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.TICKET.ToString(), AccessPermission.IsView))
                {
                    ViewBag.ViewPermission = "TICKET";

                    return RedirectToAction("AccessDenied", "Base", new { viewPermission = "ticket" });
                }
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult GetGridData([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                int CreatedBy = SessionHelper.UserId;
                string filterCondition = "";
                foreach (IFilterDescriptor filter in request.Filters)
                {
                    FilterDescriptor filterDesc = (FilterDescriptor)filter;
                    filterCondition = filterDesc.Value.ToString();
                }

                IQueryable<TicketModel> List;
                int totalCount = 0;
                if (SessionHelper.UserId == 1)
                {
                    List = _ticketService.GetAllTicketsAdmin(request.PageSize, request.Page, request.Sorts, filterCondition);
                    var GetCount = _db.Tickets.Count();
                    //var alltickets = _ticketService.GetAllTicketsAdmin(request.PageSize, request.Page, request.Sorts, filterCondition);
                    totalCount = GetCount;
                }
                else
                {
                    List = _ticketService.GetAllTickets(request.PageSize, request.Page, request.Sorts, filterCondition);
                    totalCount = List.Count();
                }

                var sortedList = ApplyOrdersSorting(List, request.Sorts, request.PageSize, request.Page);
                var returnData = new DataSourceResult()
                {
                    Data = sortedList,
                    Total = totalCount
                };

                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*  public ActionResult GetGridData([DataSourceRequest] DataSourceRequest request)
          {
              try
              {
                  int CreatedBy = SessionHelper.UserId;
                  string filterCondition = "";
                  foreach (IFilterDescriptor filter in request.Filters)
                  {
                      FilterDescriptor filterDesc = (FilterDescriptor)filter;
                      filterCondition = filterDesc.Value.ToString();
                  }
                  IQueryable<TicketModel> List;
                  if (SessionHelper.UserId == 1)
                  {
                      List = _ticketService.GetAllTicketsAdmin(request.PageSize, request.Page, request.Sorts, filterCondition);              
                  }
                  else
                  {
                      List = _ticketService.GetAllTickets(request.PageSize, request.Page, request.Sorts, filterCondition);
                  }

                  DataSourceResult result = List.ToDataSourceResult(request);
                  var sortedlist = ApplyOrdersSorting(List, request.Sorts, request.PageSize, request.Page);
                  var returndata = new DataSourceResult()
                  {
                      Data = sortedlist,
                      Total = List.ToList().Count > 0 ? List.ToList().Count: 0
                  };





                  return Json(returndata, JsonRequestBehavior.AllowGet);
              }
              catch (Exception ex)
              {
                  throw ex;
              }
          }*/


        public IQueryable<TicketModel> ApplyOrdersSorting(IQueryable<TicketModel> data, IList<SortDescriptor> sortDescriptors, int PageSize, int Page)
        {
            if (sortDescriptors != null && sortDescriptors.Any())
            {
                foreach (SortDescriptor sortDescriptor in sortDescriptors)
                {
                    data = (IQueryable<TicketModel>)AddSortExpression(data, sortDescriptor.SortDirection, sortDescriptor.Member, PageSize, Page);
                }
            }
            return data;
        }

        public IQueryable<TicketModel> AddSortExpression(IQueryable<TicketModel> data, ListSortDirection
                        sortDirection, string memberName, int PageSize, int Page)
        {
            if (sortDirection == ListSortDirection.Ascending)
            {
                switch (memberName)
                {
                    case "AssignedToName":
                        data = data.OrderBy(order => order.AssignedToName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "StatusName":
                        data = data.OrderBy(order => order.StatusName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "TicketName":
                        data = data.OrderBy(order => order.TicketName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "DescriptionData":
                        data = data.OrderBy(order => order.DescriptionData).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;

                    case "TypeName":
                        data = data.OrderBy(order => order.TypeName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "PriorityName":
                        data = data.OrderBy(order => order.PriorityName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "CreatedOn":
                        data = data.OrderBy(order => order.CreatedOn).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                }
            }
            else
            {
                switch (memberName)
                {
                    case "AssignedToName":
                        data = data.OrderByDescending(order => order.AssignedToName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "StatusName":
                        data = data.OrderByDescending(order => order.StatusName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "TicketName":
                        data = data.OrderByDescending(order => order.TicketName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "DescriptionData":
                        data = data.OrderByDescending(order => order.DescriptionData).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;

                    case "TypeName":
                        data = data.OrderByDescending(order => order.TypeName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "PriorityName":
                        data = data.OrderByDescending(order => order.PriorityName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "CreatedOn":
                        data = data.OrderByDescending(order => order.CreatedOn).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                }
            }
            return data;
        }

        public ActionResult Create()
        {
            try
            {
                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.TICKET.ToString(), AccessPermission.IsAdd))
                {
                    return RedirectToAction("AccessDenied", "Base");
                }
                TicketModel model = new TicketModel();

                model.StatusDropdown = _ticketService.GetDropdownBykey("Status")
                   .Select(x => new MyDropdown() { id = x.Id, name = x.Name }).ToList();
                model.PriorityDropdown = _ticketService.GetDropdownBykey1("Priority")
                   .Select(x => new MyDropdown() { id = x.Id, name = x.Name }).ToList();
                model.TypeDropdown = _ticketService.GetDropdownBykey2("Task")
                    .Select(x => new MyDropdown() { id = x.Id, name = x.Name }).ToList();

                model.AssignedDropdown = _usersService.GetAlluser()
                    .Select(x => new MyDropdown() { id = x.Id, name = x.Name }).ToList();

                return PartialView("Create", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult Create(TicketModel model, HttpPostedFileBase imgfile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int CreatedBy = SessionHelper.UserId;
                    var ticketId = _ticketService.CreateTickets(model, CreatedBy);

                    var statusStr = _commonLookupService.GetCommonLookupById(model.StatusId).Name;
                    if (imgfile != null)
                    {
                        model.ImageName = imgfile.FileName + Path.GetExtension(imgfile.FileName);
                        imgfile.SaveAs(Server.MapPath("//Content//Uploadimage//") + model.ImageName);
                    }
                    TicketStatus obj = new TicketStatus()
                    {
                        TicketId = ticketId,
                        NewStatus = statusStr,
                        CreatedOn = DateTime.Now,
                        CreatedBy = CreatedBy
                    };
                    TicketAttachment obj1 = new TicketAttachment()
                    {
                        TicketId = ticketId,
                        Filename = model.ImageName,
                        CreatedOn = DateTime.Now,
                        CreatedBy = CreatedBy
                    };
                    EmailController objEmail = new EmailController();
                    string emailAddress = _usersService.GetUserById(model.AssignedTo).EmailId;
                    objEmail.SendMyMail(emailAddress, model.TicketName, model.DescriptionData, imgfile, ticketId);
                    ViewBag.EmployeeList = _ticketService.BindEmployee();
                    _ticketService.CreateTicketStatus(obj, CreatedBy);
                    _ticketService.CreateAttachment(obj1, CreatedBy);
                    TempData["Message"] = "Data Updated Successfully!!";

                    return RedirectToAction("Index");
                }
                else
                {
                    model.StatusDropdown = _ticketService.GetDropdownBykey("Status")
                        .Select(x => new MyDropdown() { id = x.Id, name = x.Name }).ToList();
                    model.PriorityDropdown = _ticketService.GetDropdownBykey1("Priority")
                       .Select(x => new MyDropdown() { id = x.Id, name = x.Name }).ToList();
                    model.TypeDropdown = _ticketService.GetDropdownBykey2("Task")
                        .Select(x => new MyDropdown() { id = x.Id, name = x.Name }).ToList();

                    model.AssignedDropdown = _usersService.GetAlluser()
                        .Select(x => new MyDropdown() { id = x.Id, name = x.Name }).ToList();

                    return PartialView("Create", model);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public FileResult DownloadFile(string fileName)
        {
            try
            {

                string contentType = string.Empty;
                contentType = "application/force-download";
                string fullPath = Path.Combine(Server.MapPath("~/Content/Uploadimage/") + fileName);
                return File(fullPath, contentType, fileName);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ActionResult Edit(int Id)
        {
            try
            {

                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.TICKET.ToString(), AccessPermission.IsEdit))
                {
                    return RedirectToAction("AccessDenied", "Base");
                }
                TicketModel obj = _ticketService.GetTicketsById(Id);

                obj.StatusDropdown = _ticketService.GetDropdownBykey("Status")
                .Select(x => new MyDropdown() { id = x.Id, name = x.Name }).ToList();
                obj.PriorityDropdown = _ticketService.GetDropdownBykey1("Priority")
                   .Select(x => new MyDropdown() { id = x.Id, name = x.Name }).ToList();
                obj.TypeDropdown = _ticketService.GetDropdownBykey2("Task")
                    .Select(x => new MyDropdown() { id = x.Id, name = x.Name }).ToList();

                obj.AssignedDropdown = _usersService.GetAlluser()
                    .Select(x => new MyDropdown() { id = x.Id, name = x.UserName }).ToList();
                return View(obj);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        public ActionResult Edit(TicketModel model, HttpPostedFileBase imgfile)
        {
            try
            {
                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.TICKET.ToString(), AccessPermission.IsEdit))
                {
                    return RedirectToAction("AccessDenied", "Base");
                }
                int UpdatedBy = SessionHelper.UserId;
                var ticketId = _ticketService.UpdateTicket(model, UpdatedBy);
                var statusStr = _commonLookupService.GetCommonLookupById(model.StatusId).Name;
                if (imgfile != null)
                {
                    model.ImageName = imgfile.FileName + Path.GetExtension(imgfile.FileName);
                    imgfile.SaveAs(Server.MapPath("//Content//Uploadimage//") + model.ImageName);
                }
                TicketStatus obj = new TicketStatus()
                {
                    TicketId = ticketId.Id,
                    NewStatus = statusStr,
                    CreatedBy = UpdatedBy,
                    CreatedOn = DateTime.Now
                };
                TicketAttachment obj1 = new TicketAttachment()
                {
                    TicketId = ticketId.Id,
                    Filename = model.ImageName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = UpdatedBy
                };
                _ticketService.CreateTicketStatus(obj, UpdatedBy);
                _ticketService.CreateAttachment(obj1, UpdatedBy);
                TicketService objservice = new TicketService();
                TicketModel objmodels = objservice.UpdateTicket(model, UpdatedBy);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Display(int Id)
        {
            try
            {
                TicketModel obj = _ticketService.GetTicketsById(Id);
                return View(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult TicketDetails(int TicketId)
        {
            try
            {
                TicketModel obj = _ticketService.GetTicketsById(TicketId);
                return View(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult getTicketDetails(int id)
        {
            try
            {
                TicketModel obj = _ticketService.GetTicketsById(id);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Comment(int Id)
        {
            try
            {
                TicketCommentViewModel model = new TicketCommentViewModel();
                model.TicketId = Id;
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult getTicketComment(int id)
        {
            try
            {
                List<TicketCommentViewModel> List = _ticketService.GetAllComment(id);
                return Json(List, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public JsonResult Comment(TicketCommentViewModel model)
        {
            try
            {
                int CreatedBy = SessionHelper.UserId;
                string CreatedBy1 = SessionHelper.UserName;
                _ticketService.CreateTicketComment(model, CreatedBy, CreatedBy1);
                string message = "SUCCESS";
                return Json(new { model = message, JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult postTicketComment(int id)
        {
            try
            {
                List<TicketCommentViewModel> tickets = new List<TicketCommentViewModel>();
                return Json(tickets, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Delete(int Id)
        {
            try
            {
                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.TICKET.ToString(), AccessPermission.IsDelete))
                {
                    return RedirectToAction("AccessDenied", "Base");
                }
                _ticketService.DeleteTicket(Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult CommentIndex(int Id)
        {
            try
            {
                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.TICKET.ToString(), AccessPermission.IsView))
                {
                    return RedirectToAction("AccessDenied", "Base");
                }
                List<TicketCommentViewModel> List = _ticketService.GetAllComment(Id);
                return View(List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

