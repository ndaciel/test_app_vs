using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace Core.Manager
{
    public class EmailFacade
    {
        public static void SendEmail(string email)
        {
            var body = "<p>Email From: PT. TirtaKencana</p>" +

                          "<p>Courier tidak sampai didalam radius</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(email));  // replace with valid value 
            message.From = new MailAddress("christopher@mobilecom.co.id");  // replace with valid value
            message.Subject = "Registration Confirmation";
            message.Body = string.Format(body);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "christopher@mobilecom.co.id",  // replace with valid value
                    Password = "com321mobile"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "mail.mobilecom.co.id";
                smtp.Port = 26;
                smtp.EnableSsl = false;
                smtp.Send(message);

            }
        }
    }
}
