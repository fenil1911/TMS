using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TMS.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: DashBoard
        public ActionResult Index()
        {
            ViewBag.ViewPermission = "TICKET";
            ViewBag.ViewPermission = "TMS";
            
            return View();
        }
    }
}