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
    public class FormController : BaseController
    {
        private readonly FormsService _formsService;
        public FormController()
        {
            _formsService = new FormsService();
        }
        public ActionResult Index(int? page)
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.FORMMASTER.ToString(), AccessPermission.IsView))
            {

            }
            List<FormModel> FormsList = _formsService.GetAllForms();
            return View(FormsList.ToPagedList(page ?? 1, 6));
        }
        [HttpGet]
        public ActionResult Create(int? id)
        {
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

            }

            int userId = SessionHelper.UserId;
            FormModel model = new FormModel();
            if (id.HasValue)
            {
                var formDetail = _formsService.GetFormsById(id.Value);
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

        [HttpPost]
        public ActionResult Create(FormModel model)
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
                return View(model);
            }
            _formsService.SaveUpdateForm(model);
            TempData["Message"] = "Data Saved Successfully!!";
            return RedirectToAction("Index");
        }

        private void BindDropdown(ref FormModel model)
        {
            BindParentForm(ref model);
        }

        public FormModel BindParentForm(ref FormModel model)
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
        public JsonResult CheckDuplicateFormAccessCode(string FormAccessCode, int Id)
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
    }
}