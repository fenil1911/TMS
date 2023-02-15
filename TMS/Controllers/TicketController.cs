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

namespace TMS.Controllers
{
    public class TicketController : BaseController
    {
        private readonly TicketService _ticketService;
        private readonly CommonLookupService _commonLookupService;
        public TicketController()
        {
            _ticketService = new TicketService();
            _commonLookupService = new CommonLookupService();
        }

        public ActionResult Index()
        {
            List<TicketModel> List;
            try
            {
                List = _ticketService.GetAllTickets();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(List);
        }

        public ActionResult Create()
        {
            TicketModel model = new TicketModel();

            model.StatusDropdown = _ticketService.GetDropdownBykey("Status")
               .Select(x => new MyDropdown() { id = x.Id, name = x.Name }).ToList();
            model.PriorityDropdown = _ticketService.GetDropdownBykey1("Priority")
               .Select(x => new MyDropdown() { id = x.Id, name = x.Name }).ToList();
            model.TypeDropdown = _ticketService.GetDropdownBykey2("Task")
                .Select(x => new MyDropdown() { id = x.Id, name = x.Name }).ToList();
            return PartialView("Create", model);
        }

        [HttpPost]
        public ActionResult Create(TicketModel model, HttpPostedFileBase imgfile)
        {
            var ticketId = _ticketService.CreateTickets(model);

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
                CreatedOn = DateTime.Now
            };
            TicketAttachment obj1 = new TicketAttachment()
            {
                TicketId = ticketId,
                Filename = model.ImageName,
                CreatedOn = DateTime.Now
            };
            _ticketService.CreateTicketStatus(obj);
            _ticketService.CreateAttachment(obj1);
            return RedirectToAction("Index");
        }
        public FileResult DownloadFile(string fileName)
        {
            string contentType = string.Empty;
            contentType = "application/force-download";
            string fullPath = Path.Combine(Server.MapPath("~/Content/Uploadimage/") + fileName);
            return File(fullPath, contentType, fileName);
        }
        public ActionResult Edit(int Id)
        {
            TicketModel obj = _ticketService.GetTicketsById(Id);
            return View(obj); 
        }

        [HttpPost]
        public ActionResult Edit(TicketModel model, HttpPostedFileBase imgfile)
        {
            var ticketId = _ticketService.UpdateTicket(model);

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
                CreatedOn = DateTime.Now
            };
            TicketAttachment obj1 = new TicketAttachment()
            {
                TicketId = ticketId.Id,
                Filename = model.ImageName,
                CreatedOn = DateTime.Now
            };
            TicketService objservice = new TicketService();
            TicketModel objmodels = objservice.UpdateTicket(model);
            return RedirectToAction("Index");
        }
        public ActionResult Display(int Id)
        {
            TicketModel obj = _ticketService.GetTicketsById(Id);
           
            return View(obj);   
           
        }
        public ActionResult Display1()
        {
            TicketCommentViewModel model = new TicketCommentViewModel();
            return View(model);
                
        }
        [HttpPost]
        public ActionResult Comment(TicketCommentViewModel model)
        {

            _ticketService.CreateTicketComment(model);
            return View();


        }
    }
}

