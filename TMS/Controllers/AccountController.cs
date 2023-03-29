using System;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using TMS.Data.Database;
using TMS.Model;
using WebMatrix.WebData;
using TMS.Data;
using TMS.Helper;
using TMS.Service;
using TMS.Model.General;
using static TMS.Model.AccountModel;


namespace TMS.Controllers
{
    public class AccountController : Controller
    {
        TMSEntities _db;
        public readonly UsersService _usersService;
        public readonly RoleService _roleService;
        public AccountController()
        {
            _db = new TMSEntities();
            _usersService = new UsersService();
            _roleService = new RoleService();
        }
        [AllowAnonymous]
        public ActionResult Login(string ReturnUrl)
        {
            Model.LoginModel model = new Model.LoginModel();
            if (SessionHelper.UserId > 0)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Model.LoginModel model, string ReturnUrl = "")
        {
            try
            {
                if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
                {
                    var userID = WebSecurity.GetUserId(model.UserName);
                    var rolecode = Roles.GetRolesForUser(model.UserName);
                    var rolecodestring = rolecode[0];
                    //var roleId = Roles.GetUsersInRole(rolecode).ToString();






                    if (model.Password == "123")
                    {

                        return RedirectToAction("ChangePassword", "Account", userID);
                    }
                    else
                    {
                        
                        SessionHelper.UserId = userID;
                        //SessionHelper.RoleCode = Roles.GetRolesForUser(model.RoleCode).FirstOrDefault();
                        SessionHelper.RoleName = rolecodestring;
                        SessionHelper.IsAdmin = true;
                        SessionHelper.UserName = model.UserName;
                        SessionHelper.RoleCode = Roles.GetRolesForUser(model.UserName).FirstOrDefault();
                        string returnUrl = Request.QueryString["ReturnUrl"];
                        Session["UserName"] = model.UserName.ToString();
                        SessionHelper.UserId = userID;
                        var sadmin = SessionHelper.UserId;
                        var user = SessionHelper.UserId;

                        return RedirectToAction("Index", "DashBoard");
                    }
                }
                else
                {

                    ModelState.AddModelError("Password", "The User name or password provided is incorrect.");

                }
                return View(model);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Logout()
        {
            try
            {
                WebSecurity.Logout();
                Session.Abandon();
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Authorize]
        public ActionResult Register()
        {
            if (Roles.IsUserInRole(WebSecurity.CurrentUserName, "Administrator"))
                ViewBag.RoleId = 1;
            RegisterModel registerModel = new RegisterModel();
            registerModel.RoleDropdown = _roleService.GetAllRoles()
                .Select(x => new MyDropdown() { Key = x.Name, name = x.Name }).ToList();

            GetRolesForCurrentUser();
            return View();
        }
        private ActionResult GetRolesForCurrentUser()
        {
            if (Roles.IsUserInRole(WebSecurity.CurrentUserName, "Administrator"))
                ViewBag.RoleId = 1;
            RegisterModel registerModel = new RegisterModel();
            registerModel.RoleDropdown = _roleService.GetAllRoles()
                .Select(x => new MyDropdown() { Key = x.Name, name = x.Name }).ToList();
            return PartialView("GetRolesForCurrentUser", registerModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(RegisterModel registerModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isUserExists = WebSecurity.UserExists(registerModel.UserName);
                    if (isUserExists)
                    {
                        ModelState.AddModelError("UserName", "");
                    }
                    else
                    {
                        WebSecurity.CreateUserAndAccount(registerModel.UserName, registerModel.Password, new
                        {
                            FirstName = registerModel.FirstName,
                            EmailId = registerModel.EmailId,
                            LastName = registerModel.LastName,
                            CreatedOn = DateTime.Now,
                            IsActive = registerModel.IsActive,
                            IsDeleted = registerModel.IsDeleted
                           
                            
                        });
                        Roles.AddUserToRole(registerModel.UserName, registerModel.Role);
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
                else
                {
                    registerModel.RoleDropdown = _roleService.GetAllRoles()
                        .Select(x => new MyDropdown() { Key = x.Name, name = x.Name }).ToList();
                    return View("Register", registerModel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            registerModel.RoleDropdown = _roleService.GetAllRoles()
                .Select(x => new MyDropdown() { Key = x.Name, name = x.Name }).ToList();
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel changePasswordModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    bool isPasswordChanged = WebSecurity.ChangePassword(WebSecurity.CurrentUserName, changePasswordModel.OldPassword, changePasswordModel.NewPassword);
                    if (isPasswordChanged)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        ModelState.AddModelError("OldPassword", "OldPassword is not corret");
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View();
        }

    }
}

