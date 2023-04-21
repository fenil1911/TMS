using TMS.Model;
using TMS.Service;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace TMS.Controllers
{
    public class CommonLookupController : BaseController
    {
        private readonly CommonLookupService commonLookupService;

        public CommonLookupController()
        {
            commonLookupService = new CommonLookupService();
        }
        public ActionResult Index([DataSourceRequest] DataSourceRequest request)
        {
            try
            {

                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.COMMONLOOKUP.ToString(), AccessPermission.IsView))
                {
                    return RedirectToAction("AccessDenied", "Base");
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



                List<CommonLookupModel> List = commonLookupService.GetAllCommonLookup();
                DataSourceResult result = List.ToDataSourceResult(request);
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
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
            }
        }
        [HttpPost]
        public ActionResult Create(CommonLookupModel model)
        {
            try
            {
                
                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.COMMONLOOKUP.ToString(), AccessPermission.IsAdd))
                {
                    return RedirectToAction("AccessDenied", "Base");
                }
                if (ModelState.IsValid)
                {
                    int CreatedBy = SessionHelper.UserId;
                    commonLookupService.CreateCommonLookup(model, CreatedBy);
                    return View("Index");
                }
                else
                {
                    return PartialView("Create", model);
                }

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
            }
        }
        [HttpPost]
        public ActionResult Edit(CommonLookupModel commonlookupmodel)
        {
            try
            {
                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.COMMONLOOKUP.ToString(), AccessPermission.IsEdit))
                {
                    return RedirectToAction("AccessDenied", "Base");
                }
                int UpdatedBy = SessionHelper.UserId;
                if (ModelState.IsValid)
                {
                  CommonLookupModel commonlookup_model = commonLookupService.UpdateCommonLookup(commonlookupmodel, UpdatedBy);
                    TempData["Message"] = "Data Updated Successfully!!";
                    return RedirectToAction("Index");
                }
                else
                {

                    return PartialView("Edit");
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

         public ActionResult Editing_Popup()
        {
            return View();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, CommonLookupModel objcommonLookup )
        {
            int CreatedBy = SessionHelper.UserId;
            commonLookupService.CreateCommonLookup(objcommonLookup , CreatedBy);

            return Json(new[] { objcommonLookup  }.ToDataSourceResult(request, ModelState)); 
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request , int Id)
        {
                commonLookupService.DeleteCommonLookup(Id);
          

            return Json(new[] { Id }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, CommonLookupModel commonlookupmodel)
        {
            int UpdatedBy = SessionHelper.UserId;
            commonLookupService.UpdateCommonLookup(commonlookupmodel, UpdatedBy);


            return Json(new[] { commonlookupmodel }.ToDataSourceResult(request, ModelState));
        }
    }
}