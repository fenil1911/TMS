using TMS.Model;
using TMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TMS.Controllers
{
    public class UsersController : Controller
    {

        private readonly RoleService _rolesService;
        private readonly FormRoleMappingService _formRoleMapping;
        private readonly FormsService _formsService;
        public UsersController()
        {
            try
            {
                _rolesService = new RoleService();
                _formRoleMapping = new FormRoleMappingService();
                _formsService = new FormsService();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}