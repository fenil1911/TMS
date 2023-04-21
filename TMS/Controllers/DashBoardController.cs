using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Model;
using TMS.Service;

namespace TMS.Controllers
{
    public class DashBoardController : BaseController
    {
        private readonly TicketService _ticketService;

        public DashBoardController()
        {
            _ticketService = new TicketService();
        }
        public ActionResult Index()
        {
            try
            {
                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.DASHBOARD.ToString(), AccessPermission.IsView))
                {
                    return RedirectToAction("AccessDenied", "Base");
                }
                int CreatedBy = SessionHelper.UserId;
                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.DASHBOARD.ToString(), AccessPermission.IsView))
                {
                    return RedirectToAction("AccessDenied", "Base");
                }

                if (SessionHelper.UserId == 1)
                {
                    ViewBag.status = _ticketService.GetAllStatus();
                   
                    ViewBag.TotalHigh = _ticketService.TotalHighAdmin();
                    ViewBag.TotalLow = _ticketService.TotalLowAdmin();
                    ViewBag.TotalMedium = _ticketService.TotalMediumAdmin();
                    ViewBag.TotalImmediate = _ticketService.TotalImmediateAdmin();



                }
                else
                {
                    ViewBag.status = _ticketService.GetAllStatususer();
                    ViewBag.TotalHigh = _ticketService.TotalHigh();
                    ViewBag.TotalLow = _ticketService.TotalLow();
                    ViewBag.TotalMedium = _ticketService.TotalMedium();
                    ViewBag.TotalImmediate = _ticketService.TotalImmediate();
                }

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}