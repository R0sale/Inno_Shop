using Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.Net;
using Microsoft.AspNetCore.WebUtilities;

namespace UserRepository
{
    public class EmailService : IEmailService
    {
        private readonly string _baseLink;

        public EmailService(IConfiguration config)
        {
            _baseLink = config["baseLink"];
        }

        public string GenerateEmailLink(string userId, string code)
        {
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            return $"{_baseLink}/api/authentication/confirmEmail?userId={userId}&encodedToken={encodedToken}";
        }

        public string GenerateRestoreLink(string userId, string code, string newPassword)
        {
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            return $"{_baseLink}/api/authentication/confirmPassword?userId={userId}&encodedToken={encodedToken}&newPassword={newPassword}";
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "kvusov@bk.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync("smtp.mail.ru", 465, true);
                await client.AuthenticateAsync("kvusov@bk.ru", "rbnQhSqdNKqsbtgcwNrj");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
