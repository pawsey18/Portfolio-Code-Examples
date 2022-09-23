using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Mail;

namespace Service
{
    public class EmailService
    {

        public void SendMail(string email, string from, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.To.Add(email);

            mailMessage.From = new MailAddress(from);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;

            SmtpClient smtpClient = new SmtpClient("localhost");
            smtpClient.Send(mailMessage);

            return;
        }
    }
}
