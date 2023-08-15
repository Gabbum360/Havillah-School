using ApplicationServices.Services.Mail;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Entities.Events
{
    public class NewStudentRegisteredEvent : INotification
    {
        public string AdminEmail { get; set; }
        public string AdminName { get; set; }

        public string StudentFullName { get; set; }

        public string StudentEmailAddress { get; set; }

        public string StudentPhoneNumber { get; set; }

        public string DatOfRegistration { get; set; }
    }

    public class NewCustomerRegisteredEventHandler : INotificationHandler<NewStudentRegisteredEvent>
    {
        private readonly IMailManager _mailManager;
        private readonly ILogger<NewCustomerRegisteredEventHandler> _logger;

        public NewCustomerRegisteredEventHandler(IMailManager mailManager, ILogger<NewCustomerRegisteredEventHandler> logger)
        {
            _mailManager = mailManager;
            _logger = logger;
        }

        public async Task Handle(NewStudentRegisteredEvent notification, CancellationToken cancellationToken)
        {
            //Call and send OTP here
            try
            {
                _mailManager.BuildMessageToAdminOnStudentRegistrationMessage(notification.AdminEmail, notification.StudentFullName,
                    notification.StudentEmailAddress, notification.StudentPhoneNumber, notification.DatOfRegistration);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured sending email to Admin user");
            }
        }
    }
}
