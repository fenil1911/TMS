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
using System.Data.Entity;

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
        public ActionResult Login(string ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl;

            // Check for password reset success message
            var passwordResetSuccess = TempData["PasswordResetSuccess"];
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

                        return RedirectToAction("ChangePassword", "Account");
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
        
        [AllowAnonymous]
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
                        TempData["AlertMessage"] = "The username already exists.";
                        TempData["AlertType"] = "alert-danger";
                        return RedirectToAction("Register");

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
                        WebSecurity.Logout();
                        Session.Abandon();
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

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotPassword(string EmailId)
        {
            string message = "";
            bool status = false;

            var userexist = _usersService.GetEmailById(EmailId);
            var email = userexist.EmailId;
            var account = email;
            if (account != null)
            {
                // create a new user with resetCode as the password
                string resetCode = Guid.NewGuid().ToString();

                // generate a password reset token for the user
                
                string token = WebSecurity.GeneratePasswordResetToken(userexist.UserName);

                /*string resetCode = Guid.NewGuid().ToString();
                string token = WebSecurity.GeneratePasswordResetToken();*/
                SendVerificationLinkEmail(account, token, "ResetPassword");
                userexist.ResetPasswordCode = token;
                _db.Entry(userexist).State = EntityState.Modified;
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

            var fromEmail = new MailAddress("fenil.aakashinfo@gmail.com", "TMS");//change with your gmail id
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "xzsytfjppkjaihqx"; // Replace with actual password

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
         [AllowAnonymous]
        public ActionResult ResetPassword(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }
            var userexist = _usersService.GetroleById(id);
            var rolecode = userexist.ResetPasswordCode;
            var user = _db.Users.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
            if (rolecode != null)
            {
                ResetPasswordModel model = new ResetPasswordModel();
                model.ResetCode = id;
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        
        [AllowAnonymous]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                var user = _db.Users.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                if (user != null)
                {
                    bool isPasswordChanged = WebSecurity.ResetPassword(model.ResetCode, model.NewPassword);

                    if (isPasswordChanged)
                    {
                        TempData["PasswordResetSuccess"] = "Your password has been successfully reset. Please log in with your new password.";
                        return Json(new { success = true, redirectUrl = Url.Action("Logout", "Account") });
                    }
                    else
                    {
                        message = "Failed to reset password.";
                    }
                }
                else
                {
                    message = "Invalid reset code.";
                }
            }
            else
            {
                message = "Invalid input data.";
            }

            return Json(new { success = false, message = message });
        }

   

    }

}

