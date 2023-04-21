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
    public class FormController : BaseController
    {
        private readonly FormsService _formsService;
        public FormController()
        {
            _formsService = new FormsService();
        }
        public ActionResult Index()
        {
            ViewBag.ViewPermission = "TICKET";
            try
            {

                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.FORMMASTER.ToString(), AccessPermission.IsView))
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
                List<FormModel> FormsList = _formsService.GetAllForms();
                DataSourceResult result = FormsList.ToDataSourceResult(request);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Create(int? id)
        {
            try
            {
                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.FORMMASTER.ToString(), AccessPermission.IsAdd))
                {
                    return RedirectToAction("AccessDenied", "Base");
                }
                string actionPermission = "";
                if (id == null)
                {
                    actionPermission = AccessPermission.IsAdd;
                }
                else if ((id ?? 0) > 0)
                {
                    actionPermission = AccessPermission.IsEdit;
                }

                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.FORMMASTER.ToString(), actionPermission))
                {
                    return RedirectToAction("AccessDenied", "Base");
                }

                int userId = SessionHelper.UserId;
                FormModel model = new FormModel();
                if (id.HasValue)
                {
                    int CreatedBy = SessionHelper.UserId;
                    var formDetail = _formsService.GetFormsById(id.Value ,  CreatedBy);
                    if (formDetail != null)
                    {
                        model.Id = id.Value;
                        model.Id = formDetail.Id;
                        model.Name = formDetail.Name;
                        model.NavigateURL = formDetail.NavigateURL;
                        model.ParentFormId = formDetail.ParentFormId;
                        model.FormAccessCode = formDetail.FormAccessCode;
                        model.DisplayOrder = formDetail.DisplayOrder;
                        model.IsDisplayMenu = formDetail.IsDisplayMenu;
                        model.IsActive = formDetail.IsActive;
                    }
                }
                BindDropdown(ref model);
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Create(FormModel model)
        {
            try
            {

                string actionPermission = "";
                if (model.Id == 0)
                {
                    actionPermission = AccessPermission.IsAdd;
                }
                else if (model.Id > 0)
                {
                    actionPermission = AccessPermission.IsEdit;
                }
               

                int userId = SessionHelper.UserId;

                if (!ModelState.IsValid)
                {
                    BindDropdown(ref model);
                    return PartialView("Create",model);
                }
                _formsService.SaveUpdateForm(model);
                TempData["Message"] = "Data Saved Successfully!!";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindDropdown(ref FormModel model)
        {
            BindParentForm(ref model);
        }
        public FormModel BindParentForm(ref FormModel model)
        {
            try
            {

                int currentFormId = model.Id;
                var getparentform = _formsService.GetAllForms().Where(f => f.Id != currentFormId).Select(a => new FormModel { Id = a.Id, Name = a.Name }).OrderBy(a => a.Name);
                model._ParentFormList.Add(new SelectListItem() { Text = "Select Parent", Value = "" });
                foreach (var item in getparentform)
                {
                    model._ParentFormList.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
                }
                return model;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public JsonResult CheckDuplicateFormAccessCode(string FormAccessCode, int Id)
        {
            try
            {

                var checkduplicate = _formsService.CheckDuplicateFormAccessCode(FormAccessCode);
                if (Id > 0)
                {
                    checkduplicate = checkduplicate.Where(x => x.Id != Id).ToList();
                }
                if (checkduplicate.Count() > 0)
                {
                    return Json("FormAccessCode is already exist.", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ActionResult Delete(int Id)
        {
            _formsService.DeleteForm(Id);

            return RedirectToAction("Index");
        }
    }
}
