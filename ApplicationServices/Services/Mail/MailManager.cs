using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApplicationServices.Services.Mail.MailDto;

namespace ApplicationServices.Services.Mail
{
    public class MailManager :IMailManager
    {
        private string  _message;
        private string _subject;
        private string _fromEmail;
        private string _toEmail;

        public MailManager WithToEmail(string toEmail)
        {
            this._toEmail = toEmail;
            return this;
        }
        public MailManager WithFromEmail(string fromEmail)
        {
            this._fromEmail = fromEmail;
            return this;
        }
        public MailManager WithSubject(string subject)
        {
            this._subject = subject;
            return this;
        }

        public MailManager BuildMessageToAdminOnStudentRegistrationMessage(string adminName, string studentFullName, string studentEmail, string studentPhoneNumber, string dateOfRegistration)
        {
            return this;
        }
        public MailManager BuildMessageToAdminOnAdminRegistrationMessage(string name, string email)
        {
            return this;
        }
        /*public MailBuilder BuildBuildNewTicketRaisedMessage(string adminName, Guid ticketId, string customerName, DateTime dateTimeOfTicket, string issueCategory, string descriptionOfIssue)
        {
            var rawMailTemplate = FileHelper.ExtractMailTemplate("page12-ticket-raised.html");
            _message = rawMailTemplate.Replace("{Admin's Name}", adminName)
                .Replace("{Ticket ID}", ticketId.ToString())
                .Replace("{Customer's Name}", customerName)
                .Replace("{Ticket Date and Time}", dateTimeOfTicket.ToString())
                .Replace("{Category of the Issue}", issueCategory)
                .Replace("{Brief Description}", descriptionOfIssue);

            return this;
        }*/
        public MailObject BuildMailDto()
        {
            return new MailObject()
            {
                BodyAmp = _message,
                CharSet = "utf-8",
                From = _fromEmail,
                IsTransactional = true,
                To = _toEmail,
                Sender = _fromEmail,
                Subject = _subject
            };
        }

        public static implicit operator MailObject(MailManager builder)
        {
            return builder.BuildMailDto();
        }
    }
}
