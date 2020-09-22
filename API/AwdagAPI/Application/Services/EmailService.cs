using Application.Interfaces;
using Domain.Models.Entities;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.IO;
using System.Net;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Web;
using Application.Infrastructure;
using Application.Utilities;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Application.Services
{
    public class EmailService : Service, IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
        {
            _configuration = configuration;
        }

        public async Task<ServiceResponse> SendEmailAfterRegistrationAsync(ApplicationUser userToRegister,
            string generatedEmailConfirmationToken, string baseUrl, string language)
        {
            var message = PrepareMessage(userToRegister);

            var encodedToken = HttpUtility.UrlEncode(generatedEmailConfirmationToken);
            string url = $"{baseUrl}?userId={userToRegister.Id}&confirmationCode={encodedToken}";

            SetAfterRegistrationMessageContent(message, userToRegister, language, url);

            var sent = await SendAsync(message);
            if (sent)
                return new ServiceResponse(HttpStatusCode.OK);

            return new ServiceResponse(HttpStatusCode.BadRequest, new[] { "Wystąpił błąd podczas wysyłania maila" });
        }

        public async Task<ServiceResponse> SendPasswordResetEmailAsync(ApplicationUser user, string passwordResetToken, string baseUrl, string language)
        {
            var message = PrepareMessage(user);
            var encodedToken = HttpUtility.UrlEncode(passwordResetToken);
            string url = $"{baseUrl}?userId={user.Id}&passwordResetCode={encodedToken}";

            SetPasswordResetMessageContent(message, language, url);

            var sent = await SendAsync(message);

            if (sent)
                return new ServiceResponse(HttpStatusCode.OK);

            return new ServiceResponse(HttpStatusCode.BadRequest, new[] { "Wystąpił błąd podczas wysyłania maila" });
        }

        private void SetPasswordResetMessageContent(MimeMessage message, string language, string url)
        {
            string htmlTemplatePath;

            switch (language.ToLower())
            {
                case "pl":
                    message.Subject = "Reset hasła";
                    htmlTemplatePath = Constants.SendPasswordResetEmailPLPath;
                    break;
                case "en":
                    message.Subject = "Password reset";
                    htmlTemplatePath = Constants.SendPasswordResetEmailENPath;
                    break;
                default:
                    message.Subject = "Password reset";
                    htmlTemplatePath = Constants.SendPasswordResetEmailENPath;
                    break;
            }

            var builder = new BodyBuilder();

            using (StreamReader sourceReader = File.OpenText(htmlTemplatePath))
            {
                builder.HtmlBody = sourceReader.ReadToEnd();
            }

            builder.HtmlBody = string.Format(builder.HtmlBody, url);
            message.Body = builder.ToMessageBody();
        }

        private void SetAfterRegistrationMessageContent(MimeMessage message, ApplicationUser user, string language, string url)
        {
            string htmlTemplatePath;

            switch (language.ToLower())
            {
                case "pl":
                    message.Subject = "Witaj w systemie CodeTeam";
                    htmlTemplatePath = Constants.SendEmailAfterRegistrationPLPath;
                    break;
                case "en":
                    message.Subject = "Welcome to the CodeTeam System";
                    htmlTemplatePath = Constants.SendEmailAfterRegistrationENPath;
                    break;
                default:
                    message.Subject = "Welcome to the CodeTeam System";
                    htmlTemplatePath = Constants.SendEmailAfterRegistrationENPath;
                    break;
            }

            var builder = new BodyBuilder();

            using (StreamReader sourceReader = File.OpenText(htmlTemplatePath))
            {
                builder.HtmlBody = sourceReader.ReadToEnd();
            }

            builder.HtmlBody = string.Format(builder.HtmlBody, user.FirstName, user.LastName, url);
            message.Body = builder.ToMessageBody();
        }

        private async Task<bool> SendAsync(MimeMessage message)
        {
            try
            {
                using var client = new SmtpClient();
                await client.ConnectAsync(_configuration["Email:host"], Int32.Parse(_configuration["Email:port"]), Boolean.Parse(_configuration["Email:useSsl"]));
                await client.AuthenticateAsync(_configuration["Email:userName"], _configuration["Email:password"]);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private MimeMessage PrepareMessage(ApplicationUser user)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_configuration["Email:name"], _configuration["Email:address"]));
            message.To.Add(new MailboxAddress($"{user.FirstName} {user.LastName}", user.Email));
            return message;
        }
    }
}