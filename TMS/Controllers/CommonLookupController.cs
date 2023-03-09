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
                    return RedirectToAction("AccessDenied", "Base");
                }

                List<CommonLookupModel> List = commonLookupService.GetAllCommonLookup();
                return View(List);

            }
            catch (Exception ex)
            {

                throw ex;
            }}
        public ActionResult Create()
        {
            try
            {

                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.COMMONLOOKUP.ToString(), AccessPermission.IsAdd))
                {
                    return RedirectToAction("AccessDenied", "Base");
                }
                CommonLookupModel model = new CommonLookupModel();
                return PartialView("Create", model);

            }
            catch (Exception ex)
            {

                throw ex;
            } }
        [HttpPost]
        public ActionResult Create(CommonLookupModel model)
        {
            try
            {

                int CreatedBy = SessionHelper.UserId;
                commonLookupService.CreateCommonLookup(model, CreatedBy);
                return View();

            }
            catch (Exception ex)
            {

                throw ex;
            }}
        public ActionResult Edit(int Id)
        {
            try
            {

                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.COMMONLOOKUP.ToString(), AccessPermission.IsEdit))
                {
                    return RedirectToAction("AccessDenied", "Base");
                }
                CommonLookupModel EditCommonLookup = commonLookupService.GetCommonLookupById(Id);
                return PartialView("Edit", EditCommonLookup);

            }
            catch (Exception ex)
            {

                throw ex;
            } }
        [HttpPost]
        public ActionResult Edit(CommonLookupModel commonlookupmodel)
        {
            try
            {
                int UpdatedBy = SessionHelper.UserId;
                if (ModelState.IsValid)
                {
                    CommonLookupModel commonlookup_model = commonLookupService.UpdateCommonLookup(commonlookupmodel, UpdatedBy);
                    TempData["Message"] = "Data Updated Successfully!!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Index");
                }
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
                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.COMMONLOOKUP.ToString(), AccessPermission.IsDelete))
                {
                    return RedirectToAction("AccessDenied", "Base");
                }
                commonLookupService.DeleteCommonLookup(Id);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}