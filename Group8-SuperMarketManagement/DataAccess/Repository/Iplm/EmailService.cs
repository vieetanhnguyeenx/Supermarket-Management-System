using MimeKit.Text;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Security;
using MailKit.Net.Smtp;
using Org.BouncyCastle.Crypto.Macs;

namespace DataAccess.Repository.Iplm
{
    public class EmailService : IEmailService
    {
        public void Send(string to, string html)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("xuankhbm2@gmail.com"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = "Thanh Toán";
            email.Body = new TextPart(TextFormat.Html) { Text = html };
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.elasticemail.com", 2525, SecureSocketOptions.StartTls);
            smtp.Authenticate("xuankhbm2@gmail.com", "3BEB23014D546BC276A69BB03A2DFBDF900A");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
