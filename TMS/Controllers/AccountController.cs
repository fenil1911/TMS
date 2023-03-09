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
                return RedirectToAction("Index", "Account");
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Model.LoginModel model, string ReturnUrl = "")
        {


            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                var userID = WebSecurity.GetUserId(model.UserName);
                if (model.Password == "123")
                {

                    return RedirectToAction("ChangePassword", "Account", userID);
                }
                else
                {
                    SessionHelper.UserId = userID;
                    SessionHelper.IsAdmin = true;
                    SessionHelper.UserName = model.UserName;
                    SessionHelper.RoleName = Roles.GetRolesForUser(model.UserName).FirstOrDefault();
                    string returnUrl = Request.QueryString["ReturnUrl"];
                    Session["UserName"] = model.UserName.ToString();
                    SessionHelper.UserId = userID;
                    var sadmin = SessionHelper.UserId;


                    return RedirectToAction("Index", "DashBoard");
                }
            }

            else
            {
                ModelState.AddModelError(" ", "The User name or password provided is incorrect.");
                //  TempData["Error"] = Constants.EmailCodes.ERRORMSG;
                return View(model);
            }
        }
        public ActionResult Logout()
        {
            WebSecurity.Logout();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        [Authorize]
        public ActionResult Register()
        {
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
            return View(registerModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(RegisterModel registerModel)
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
                       
                        CreatedOn = DateTime.Now
                    });
                    Roles.AddUserToRole(registerModel.UserName, registerModel.Role);
                    return RedirectToAction("Index", "Dashboard");
                }

            }
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
            if (ModelState.IsValid)
            {
                bool isPasswordChanged = WebSecurity.ChangePassword(WebSecurity.CurrentUserName, changePasswordModel.OldPassword, changePasswordModel.NewPassword);
                if (isPasswordChanged)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "OldPassword is not corret");
                }
            }
            return View();
        }



        /*[Authorize]
        public ActionResult ChangePassword()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {

            var currentUserId = SessionHelper.UserId;
            var message = "";
            if (ModelState.IsValid)
            {
                var oldpass = Crypto.Hash(model.OldPassword);
                var obj = (from w in _db.Users
                           where w.UserId == currentUserId
                           select w).FirstOrDefault();
                if (obj.Password == oldpass)
                {
                    if (obj != null)
                    {
                        obj.Password = Crypto.Hash(model.NewPassword);
                        _db.Configuration.ValidateOnSaveEnabled = false;
                        _db.SaveChanges();
                        return Json(true, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            return RedirectToAction("Index", "Dashboard");
        }*/


    }


}

