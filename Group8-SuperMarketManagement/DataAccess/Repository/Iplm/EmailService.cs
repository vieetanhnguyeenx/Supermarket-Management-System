using MimeKit.Text;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Security;
using System.Net.Security;
using MailKit.Net.Smtp;
using Org.BouncyCastle.Crypto.Macs;

namespace DataAccess.Repository.Iplm
{
    public class EmailService : IEmailService
    {
        public void Send(string to, string from, string subject, string body)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress(from, from));
            mailMessage.To.Add(new MailboxAddress(to, to));
            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart("plain")
            {
                Text = "<h1>Thank you for purchasing at our store</h1>"
            };

            using (var smtpClient = new SmtpClient())
            {
                // Sử dụng cổng 465 cho SMTPS hoặc 587 cho STARTTLS
                smtpClient.Connect("smtp.elasticemail.com", 465, true);

                // Kích hoạt SSL/TLS
                

                // Xác thực bằng phương thức phù hợp
                smtpClient.Authenticate("xuankhbm2@gmail.com", "9C85111C165AEE83EF3C72142E1FF672CD1B");
                smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
            }
        }
    }
}
