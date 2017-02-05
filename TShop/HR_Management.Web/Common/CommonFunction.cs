using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Linq;

namespace HR_Management.Web.Common
{
    public static class CommonFunction
    {
        public static string ErrorMessage(string prefixHeading, string strMessage)
        {
            var strBuilder = new StringBuilder();

            //strBuilder.AppendFormat("<div class=\"alert alert-danger fade in\" id=\"alertBox\">");
            //strBuilder.AppendFormat("<button data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>");
            strBuilder.AppendFormat("<strong>" + prefixHeading + "</strong> " + strMessage + "</div>");

            return strBuilder.ToString();
        }
        public static string SuccessMessage(string prefixHeading, string strMessage)
        {
            var strBuilder = new StringBuilder();
            //strBuilder.AppendFormat("<div class='alert alert-success'>");
            //strBuilder.AppendFormat("<button data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>");
            strBuilder.AppendFormat("<strong>" + prefixHeading + "</strong> " + strMessage + "</div>");
            return strBuilder.ToString();
        }
        public static string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }

            //return ByteArraysEqual(buffer3, buffer4);
            return buffer3.SequenceEqual(buffer4);
            //return UnsafeCompare(buffer3, buffer4);
        }


        public static bool SendEmail(string to, string from, String cc, String bcc, String subject, String bodyTemplate)
        {
            try
            {
                System.Net.Mail.MailMessage mymail = new System.Net.Mail.MailMessage();
                mymail.To.Add(to);
                mymail.From = new MailAddress(from, "Flexsee Booking");
                if (!String.IsNullOrEmpty(cc))
                {
                    mymail.CC.Add(cc);
                }

                if (!String.IsNullOrEmpty(bcc))
                {
                    mymail.Bcc.Add(bcc);
                }

                mymail.Subject = subject;
                mymail.Body = bodyTemplate;
                mymail.IsBodyHtml = true;
                mymail.Priority = MailPriority.High;

                SmtpClient client = new SmtpClient();
                client.Host = ConfigurationManager.AppSettings["smtphost"].ToString();

                client.Port = 587;
                client.EnableSsl = false;
                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["smtpusername"].ToString(), ConfigurationManager.AppSettings["smtppassword"].ToString());
                client.Send(mymail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool SendAdminEmail(string To, string Subject, string BodyTemplate)
        {
            return CommonFunction.SendEmail(To, AdminEmail, string.Empty, string.Empty, Subject, BodyTemplate);
        }
        public static bool SendByAdminGmail(string ToEmail, string Subject, string Body)
        {
            try
            {
                //SmtpClient smtp = new SmtpClient();
                //smtp.Host = GmailHost;
                //smtp.Port = GmailPort;
                //smtp.EnableSsl = true;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.UseDefaultCredentials = false;
                //smtp.Credentials = new NetworkCredential(AdminGmail, AdminGmailPwd);
                var client = new SmtpClient();

                using (var message = new MailMessage(AdminGmail, ToEmail))
                {
                    message.Subject = Subject;
                    message.Body = Body;
                    message.IsBodyHtml = true;
                    client.SendMailAsync(message);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string ReadHtmlTemplate(string FileName)
        {
            string htmlstring = string.Empty;
            if (string.IsNullOrEmpty(FileName) == false)
            {
                if (FileName.ToLower() == "registration")
                    htmlstring = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Templates/Registration.html"));
                else if (FileName.ToLower() == "forgotpassword")
                    htmlstring = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Templates/ForgotPassword.html"));
                else if (FileName.ToLower() == "order")
                    htmlstring = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Templates/UserOrder.html"));
            }
            return htmlstring;
        }



        #region "Global Properties"
        public static string AdminEmail
        {
            get { return ConfigurationManager.AppSettings["AdminEmail"].ToString(); }
        }

        public static string AdminGmail
        {
            get { return ConfigurationManager.AppSettings["admingmail"].ToString(); }
        }
        public static string AdminGmailPwd
        {
            get { return ConfigurationManager.AppSettings["admingmailpwd"].ToString(); }
        }
        public static string GmailHost
        {
            get { return ConfigurationManager.AppSettings["gmailHost"].ToString(); }
        }
        public static int GmailPort
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["gmailPort"]); }
        }
        public static string SiteName
        {
            get { return ConfigurationManager.AppSettings["ProjectName"].ToString(); }
        }
        public static int PageSize
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]); }
        }
        #endregion
    }
    public class ResponseObject
    {
        public string IsSuccess { get; set; }

        public string StrResponse { get; set; }
    }
}