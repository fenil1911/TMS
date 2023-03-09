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

namespace TMS.Controllers
{
    public class TicketController : BaseController
    {
        private readonly TicketService _ticketService;
        private readonly CommonLookupService _commonLookupService;
        private readonly UsersService _usersService;
        public TicketController()
        {
            _ticketService = new TicketService();
            _commonLookupService = new CommonLookupService();
            _usersService = new UsersService();
        }
        public ActionResult Index([DataSourceRequest] DataSourceRequest request)
        {
            //try
            //{

                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.ROLE.ToString(), AccessPermission.IsView))
                {
                    return RedirectToAction("AccessDenied", "Base");
                }
                return View();
            //    List<TicketModel> List;
            //    try
            //    {
            //        if (SessionHelper.UserId == 1)
            //        {
            //            List = _ticketService.GetAllTicketsAdmin();
            //            DataSourceResult result = List.ToDataSourceResult(request);
            //            //return Json(result, JsonRequestBehavior.AllowGet);
            //            return View(result);
            //        }
            //        else
            //        {
            //            List = _ticketService.GetAllTickets();
            //            DataSourceResult result = List.ToDataSourceResult(request);
            //            return Json(result, JsonRequestBehavior.AllowGet);

            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //    //return View(List);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

        }

        public ActionResult GetGridData([DataSourceRequest] DataSourceRequest request)
        {
           
            List<TicketModel> List;
            if (SessionHelper.UserId == 1)
            {
                List = _ticketService.GetAllTicketsAdmin();
                DataSourceResult result = List.ToDataSourceResult(request);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if(SessionHelper.UserId != 1)
            {
                List = _ticketService.GetAllTickets();
                DataSourceResult result = List.ToDataSourceResult(request);
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return View("");
            }
        }

        public ActionResult Create()
        {
            try
            {

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
            return RedirectToAction("Index");


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
                TicketModel obj = _ticketService.GetTicketsById(Id);

                obj.AssignedDropdown = _usersService.GetAlluser()
                    .Select(x => new MyDropdown() { id = x.Id, name = x.Name }).ToList();
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

