using TMS.Model;
using TMS.Service;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace TMS.Controllers
{
    public class TicketController : BaseController
    {
        private readonly TicketService _ticketService;

        public TicketController()
        {
            _ticketService = new TicketService();
        }

        public ActionResult Index()
        {
            try
            {
                ViewBag.StatusList = _ticketService.GetDropdownBykey("Status");
                List<TicketModel> List = _ticketService.GetAllTickets();
                
                return View(List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Create()
        {
            TicketModel model = new TicketModel();
           
            model.StatusDropdown= _ticketService.GetDropdownBykey("Status")
               .Select(x => new MyDropdown() { id = x.Id, name = x.Name}).ToList();
            model.PriorityDropdown = _ticketService.GetDropdownBykey1("Priority")
               .Select(x => new MyDropdown() { id = x.Id, name = x.Name }).ToList();
            model.TypeDropdown = _ticketService.GetDropdownBykey2("Task")
                .Select(x => new MyDropdown() { id = x.Id, name = x.Name }).ToList();


            return PartialView("Create", model);
        }


        [HttpPost]
        public ActionResult Create(TicketModel model, HttpPostedFileBase file)
        {
       
            _ticketService.CreateTickets(model);
            _ticketService.CreateTicketStatus(model);
            if (file != null)
            {
                model.ImageName = file.FileName + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("//Content//Uploadimage//") + model.ImageName);
            }
            return RedirectToAction("Index");
        }
    }
}

