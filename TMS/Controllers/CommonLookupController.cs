using TMS.Model;
using TMS.Service;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TMS.Controllers
{
    public class CommonLookupController : BaseController
    {
        private readonly CommonLookupService commonLookupService;

        public CommonLookupController()
        {
            commonLookupService = new CommonLookupService();
        }
        public ActionResult Index(int? page)
        {
            try
            {
                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.COMMONLOOKUP.ToString(), AccessPermission.IsView))
                {

                }
                List<CommonLookupModel> List = commonLookupService.GetAllCommonLookup();
                return View(List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Create()
        {
            CommonLookupModel model = new CommonLookupModel();
            return PartialView("Create", model);
        }
        [HttpPost]
        public ActionResult Create(CommonLookupModel model)
        {

            commonLookupService.CreateCommonLookup(model);
            return View();


        }
        public ActionResult Edit(int Id)
        {

            CommonLookupModel EditCommonLookup = commonLookupService.GetCommonLookupById(Id);
            return PartialView("Edit", EditCommonLookup);
        }
        [HttpPost]
        public ActionResult Edit(CommonLookupModel commonlookupmodel)
        {
            try
            {
                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.COMMONLOOKUP.ToString(), AccessPermission.IsEdit))
                {
                   
                }
                if (ModelState.IsValid)
                {
                    CommonLookupModel commonlookup_model = commonLookupService.UpdateCommonLookup(commonlookupmodel);
                    TempData["Message"] = "Data Updated Successfully!!";
                    return View("Index");
                }
                else
                {
                    return Content("false");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}