using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TMS.Model;

namespace TMS.Helper
{
    public class EmailHelper : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SendEmail()
        {
            EmailModel obj = new EmailModel();
            return View(obj);
        }
        [HttpPost]
        public ActionResult SendEmail(EmailModel model)
        {

            var response = SendMyMail(model.To, model.Subject, model.Body, model.AttachmentFile);
            return View(model);



        }
        public bool SendMyMail(string To, string Subject, string Body, HttpPostedFileBase AttachmentFile)
        {
            MailMessage message = new MailMessage();
            SmtpClient Smtp = new SmtpClient(ConfigurationManager.AppSettings["MailHost"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["MailPort"].ToString()));
            NetworkCredential NetCredential = new NetworkCredential(ConfigurationManager.AppSettings["MailUsername"].ToString(), ConfigurationManager.AppSettings["MailPassword"].ToString());
            MailAddress objmailAddress;


            try
            {
                objmailAddress = new MailAddress(To);
                message.To.Add(objmailAddress);
                message.From = new MailUsername("shubham.aakashinfo@gmail.com");
                message.Subject = Subject;
                message.Body = Body;
                if (AttachmentFile != null)
                {
                    string FileName = Path.GetFileName(AttachmentFile.FileName);
                    message.Attachments.Add(new Attachment(AttachmentFile.InputStream, FileName));
                }

                message.IsBodyHtml = true;

                Smtp.UseDefaultCredentials = false;
                Smtp.Credentials = NetCredential;
                Smtp.EnableSsl = true;
                ////Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;                
                Smtp.Send(message);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}