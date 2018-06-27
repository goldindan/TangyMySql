﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Tangy.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            SmtpClient client = new SmtpClient("82.166.0.201")
            {
                UseDefaultCredentials = false,
                Credentials=new NetworkCredential("info@goldin.flox48.floxyk.co.il", "Rosario1$"),
                EnableSsl=false
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("info@goldin.flox48.floxyk.co.il")
            };
            mailMessage.To.Add(email);
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = subject;

            client.Send(mailMessage);

            return Task.CompletedTask;
        }
    }
}
