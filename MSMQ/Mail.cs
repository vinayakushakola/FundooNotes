﻿using System.Net;
using System.Net.Mail;

namespace MSMQ
{
    public class Mail
    {
        public static bool SendMail(string email, string token)
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtpserver = new SmtpClient("smtp.gmail.com");

            mailMessage.From = new MailAddress("vinayak.mailtesting@gmail.com");
            mailMessage.To.Add(email);
            mailMessage.Subject = "Reset Password";
            mailMessage.Body = token;

            smtpserver.Port = 587;
            smtpserver.Credentials = new NetworkCredential("vinayak.mailtesting@gmail.com", "@bcd.1234");
            smtpserver.EnableSsl = true;

            smtpserver.Send(mailMessage);
            return true;
        }
    }
}
