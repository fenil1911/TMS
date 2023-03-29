using TMS.Data.Database;
using TMS.Model;
using TMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace TMS.Controllers
{
    public class FormRoleMappingController : BaseController
    {

        private readonly FormRoleMappingService _formRoleMappingService;
        private readonly RoleService _roleService;
        public FormRoleMappingController()
        {
            _roleService = new RoleService();
            _formRoleMappingService = new FormRoleMappingService();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewPermission(int Id = 0)
        {
            try
            {
                string role = _roleService.GetRolesById().Name;
                if (role != "Administrator")
                {
                    return RedirectToAction("AccessDenied", "Base");
                }

                FormRoleMappingModel model = new FormRoleMappingModel();
                if (Id > 0)
                {
                    model.RoleId = Id;
                    model.RoleName = _roleService.GetRolesById().Name;
                }
                List<FormRoleMappingModel> Formrolemapping = FormRoleMapping_Read(model.RoleId);
                return View(Formrolemapping);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public JsonResult UpdatePermission(IEnumerable<FormRoleMappingModel> rolerights)
        {
            try
            {
                int CreatedBy = SessionHelper.UserId;
                int UpdatedBy = SessionHelper.UserId;
                var result = _formRoleMappingService.UpdateRoleRights(rolerights, CreatedBy, UpdatedBy);
                if (result)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost]
        public List<FormRoleMappingModel> FormRoleMapping_Read(int RoleID)
        {
            var getrolerights = _formRoleMappingService.GetAllRoleRightsById(RoleID).ToList();
            return getrolerights;

        }

    }
}