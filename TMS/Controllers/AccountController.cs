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


using System.Collections.Generic;
using System.Threading;


using System.Configuration;
using System.Net.Mail;
using System.Net;




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
                        ModelState.AddModelError("UserName", "The username already exists.");

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

            return View("Register", registerModel);

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
                throw ex;
            }
            return View();
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string EmailId)
        {
            /* string message = "";
             bool status = false;*/

            var account = _db.Users.Where(email => email.EmailId == EmailId).FirstOrDefault();
            if (account != null)
            {
                string resetCode = Guid.NewGuid().ToString();
               // SendVerificationLinkEmail(account.EmailId, resetCode, "ResetPassword");
                //account.ResetPasswordCode = resetCode;
                //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
                //in our model class in part 1
                _db.Configuration.ValidateOnSaveEnabled = false;
                _db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);

            }
        }
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/Account/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("xyz@gmail.com", "TMS");//change with your gmail id
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "yourpassword@123"; // Replace with actual password

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Your account is successfully created!";
                body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                    " successfully created. Please click on the below link to verify your account" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/>  <br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a>";
            }


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

    }

    }

