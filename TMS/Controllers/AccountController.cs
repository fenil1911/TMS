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
using static TMS.Model.AccountModel;


namespace TMS.Controllers
{
    public class AccountController : Controller
    {

        public TMSEntities _db = new TMSEntities();
        public readonly UsersService _usersService;

        public AccountController()
        {
            _usersService = new UsersService();
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
            return View( );
        }

        private ActionResult GetRolesForCurrentUser()
        {
            if (Roles.IsUserInRole(WebSecurity.CurrentUserName, "Administrator"))
                ViewBag.RoleId = 1;
            RegisterModel registerModel = new RegisterModel();
            

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
                        LastName =  registerModel.LastName,
                        Password  = Crypto.Hash(registerModel.Password),
                       
                        CreatedOn = DateTime.Now
              
                    });
                    Roles.AddUserToRole(registerModel.UserName, registerModel.Role);

                    return RedirectToAction("Index", "Dashboard");
                }

            }
            return View();
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            try
            {
                var message = "";
                if (ModelState.IsValid)
                {
                    var oldpass = Crypto.Hash(model.OldPassword);
                        var obj = _db.Users.Where(a => a.Password != model.NewPassword && a.Password == oldpass).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.Password = Crypto.Hash(model.NewPassword);
                        _db.Configuration.ValidateOnSaveEnabled = false;
                        _db.SaveChanges();
                        
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {   //message = "Something invalid";
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                return View("model");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
