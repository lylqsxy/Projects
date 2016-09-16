using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Cobra.App.Infrastructure.Contracts;
using System;
using Cobra.Infrastructure.Contracts;

namespace Cobra.App.Infrastructure.Services
{
    public class EmailSendingService : IEmailSendingService
    {
        private static string Username;
        private static string Password;
        private static string Host;
        private static int Port;
        private static bool EnableSsl;

        private static SmtpClient Client;
        static EmailSendingService()
        {
            Username = ConfigurationManager.AppSettings["Mail.SMTP.Username"];
            Password = ConfigurationManager.AppSettings["Mail.SMTP.Password"];
            Host = ConfigurationManager.AppSettings["Mail.SMTP.Host"];
            Port = int.Parse(ConfigurationManager.AppSettings["Mail.SMTP.Port"]);
            EnableSsl = bool.Parse(ConfigurationManager.AppSettings["Mail.SMTP.Ssl"] ?? bool.TrueString);

            Client = new SmtpClient()
            {
                UseDefaultCredentials = false,
                // Keep `Credentials = new NetworkCredential()` after `UseDefaultCredentials=false`, 
                // Otherwise, Credentials will always be null.
                Credentials = new NetworkCredential(Username, Password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = EnableSsl,
                Host = Host,
                Port = Port,
            };
        }
        
        //send email add by - 
        //params IdentityMessage
        public Task SendAsync(IdentityMessage message)
        {
            MailMessage Msg = new MailMessage()
            {
                From = new MailAddress(Username),
                Subject = message.Subject,
                SubjectEncoding = System.Text.Encoding.UTF8,
                Body = message.Body,
                BodyEncoding = System.Text.Encoding.UTF8,
                IsBodyHtml = true
            };
            Msg.To.Add(message.Destination);

            var MailSendingThread = new Thread(() =>
            {
                try
                {
                    Client.Send(Msg);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            });
            MailSendingThread.Start();

            return Task.FromResult(0);

        }
    }
}
