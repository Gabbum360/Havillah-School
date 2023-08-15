using ApplicationServices.Common.Options;
using ElasticEmail.Api;
using ElasticEmail.Client;
using ElasticEmail.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApplicationServices.Services.Mail.MailDto;

namespace ApplicationServices.Services.Mail
{
    public class MailService : IMailService
    {
        private static MailOption _mailOptions;
        private readonly HttpClient _client;
        private readonly ILogger<MailService> _logger;

        public MailService(IOptions<MailOption> mailOptions, HttpClient client, ILogger<MailService> logger)
        {
            _client = client;
            _logger = logger;
            _mailOptions = mailOptions.Value;
        }

        /*public void BuildPaymentSuccessfulMessage(string to, string transactionType, string name, decimal amount, string transactionTime, string transactionReference)
        {
            var builder = new MailManager();
            var mailObject = builder.WithToEmail(to)
                .WithFromEmail(_mailOptions.From)
                .WithSubject(_mailOptions.RepaymentSubject)
                .BuildPaymentSuccessfulMessage(transactionType, name, amount, transactionTime, transactionReference)
                .BuildMailDto();
            SendEmail(mailObject);
        }

        public void BuildLoanRequestMessage(string to, string name, decimal loanAmount, string loanPurpose)
        {
            var builder = new MailManager();
            var mailObject = builder.WithToEmail(to)
                .WithFromEmail(_mailOptions.From)
                .WithSubject(_mailOptions.LoanRequestSubject)
                .BuildLoanRequestMessage(name, loanAmount, loanPurpose)
                .BuildMailDto();
            SendEmail(mailObject);
        }

        public void BuildLoanApprovedByAdminMessage(string to, string name, decimal loanAmount, int loanTenure, decimal interestRate)
        {
            var builder = new MailManager();
            var mailObject = builder.WithToEmail(to)
                .WithFromEmail(_mailOptions.From)
                .WithSubject(_mailOptions.LoanApprovalSubject)
                .BuildLoanApprovedByAdminMessage(name, loanAmount, loanTenure, interestRate)
                .BuildMailDto();
            SendEmail(mailObject);
        }

        public void BuildBuildNewTicketRaisedMessage(string to, string adminName, Guid ticketId, string customerName, DateTime dateTimeOfTicket, string issueCategory, string descriptionOfIssue)
        {
            var builder = new MailManager();
            var mailObject = builder.WithToEmail(to)
                .WithFromEmail(_mailOptions.From)
                .WithSubject(_mailOptions.NewTicketRaisedSubject)
                .BuildBuildNewTicketRaisedMessage(adminName, ticketId, customerName, dateTimeOfTicket, issueCategory, descriptionOfIssue)
                .BuildMailDto();
            SendEmail(mailObject);
        }

        public void BuildNewLoanNotificationMessage(string to, string adminName, string customerName, decimal loanAmount, string loanPurpose, DateTime dateTimeOfTicket)
        {
            var builder = new MailManager();
            var mailObject = builder.WithToEmail(to)
                .WithFromEmail(_mailOptions.From)
                .WithSubject(_mailOptions.NewLoanNotificationSubject)
                .BuildNewLoanNotificationMessage(adminName, customerName, loanAmount, loanPurpose, dateTimeOfTicket)
                .BuildMailDto();
            SendEmail(mailObject);
        }

        public void BuildCustomerLoanApprovalNotificationMessage(string adminEmail, string adminName, string customerName,
            decimal loanAmount, decimal interestRate, int loanTenure, string repaymentScheduleType)
        {
            var builder = new MailManager();
            var mailObject = builder.WithToEmail(adminEmail)
                .WithFromEmail(_mailOptions.From)
                .WithSubject(_mailOptions.CustomerLoanApprovalSubject)
                .BuildCustomerLoanApprovalNotificationMessage(adminName, customerName, loanAmount, interestRate, loanTenure, repaymentScheduleType)
                .BuildMailDto();
            SendEmail(mailObject);
        }*/
        //added
        public void BuildNewOtpNotificationMessage(string to, string adminName, string studentFullName, string studentEmail, string dateOfRegistration, string studentPhoneNumber)
        {
            var builder = new MailManager();
            var mailObject = builder.WithToEmail(to)
                .WithFromEmail(_mailOptions.From)
                .WithSubject(_mailOptions.NewStudentRegistration)
                .BuildMessageToAdminOnStudentRegistrationMessage(adminName, studentFullName, studentEmail, dateOfRegistration, studentPhoneNumber)
                .BuildMailDto();
            SendEmail(mailObject);
        }

        private void SendEmail(MailObject dto)
        {
            Configuration config = new Configuration();
            // Configure API key authorization: apikey
            config.ApiKey.Add("X-ElasticEmail-ApiKey", _mailOptions.APIKey);
            var apiInstance = new EmailsApi(config);
            var to = new List<string> { dto.To };
            var recipients = new TransactionalRecipient(to: to);
            EmailTransactionalMessageData emailData = new EmailTransactionalMessageData(recipients: recipients)
            {
                Content = new EmailContent
                {
                    Body = new List<BodyPart>()
                }
            };
            BodyPart htmlBodyPart = new BodyPart
            {
                ContentType = BodyContentType.HTML,
                Charset = "utf-8",
                Content = dto.BodyAmp
            };
            BodyPart plainTextBodyPart = new BodyPart
            {
                ContentType = BodyContentType.PlainText,
                Charset = "utf-8",
                Content = dto.BodyAmp
            };
            emailData.Content.Body.Add(htmlBodyPart);
            emailData.Content.Body.Add(plainTextBodyPart);
            emailData.Content.From = dto.From;
            emailData.Content.Subject = dto.Subject;

            try
            {
                // Send Bulk Emails
                var result = apiInstance.EmailsTransactionalPost(emailData);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling EmailsApi.EmailsPost: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
