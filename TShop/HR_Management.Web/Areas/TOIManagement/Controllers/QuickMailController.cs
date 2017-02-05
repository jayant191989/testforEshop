using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Net.Mail;
using HR_Management.Web.ViewModels;

namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    public class QuickMailController : Controller
    {
        public ApplicationUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        public Task SendAsync(EmailViewModel viewModel)
        {
            string username = "testformail99@gmail.com";
            string password = "Organic&*9";
            string from = "testformail99@gmail.com";
            int port = 587;

            MailMessage mail = new MailMessage(from, viewModel.To);
            mail.Subject = viewModel.Subject;
            string Body = viewModel.Body;
            mail.Body = Body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = port;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(username, password);// Enter seders User name and password
            smtp.EnableSsl = true;

            return smtp.SendMailAsync(mail);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QuickMail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult QuickMail(EmailViewModel viewModel)
        {
            string userId = User.Identity.GetUserId();
          //  UserManager.SendEmailAsync(userId, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
            SendAsync(viewModel);
            return View();
        }
    }
}