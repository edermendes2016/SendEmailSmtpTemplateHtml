using System;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;


namespace WebApplication1.Models
{
    public class MessageServices
    {
        public async static Task SendEmailAsync(string email, string subject, string message)
        {
           
                var _email = "YourEmail@hotmail.com";
                var _epass = ConfigurationManager.AppSettings["EmailPassword"];
                var _displayName = "YourName";
                MailMessage myMessage = new MailMessage();
                myMessage.To.Add(email);
                myMessage.From = new MailAddress(_email, _displayName);
                myMessage.Subject = subject;
                myMessage.Body = message;
                myMessage.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp.live.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_email, _epass);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.SendCompleted += (s, e) => {smtp.Dispose();};


                    await smtp.SendMailAsync(myMessage);

                }

           
        }
    }
}