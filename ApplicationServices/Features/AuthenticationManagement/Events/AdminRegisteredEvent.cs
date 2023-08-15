using ApplicationServices.Entities.Events;
using ApplicationServices.Services.Mail;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.AuthenticationManagement.Events
{
    public class AdminRegisteredEvent : INotification
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class AdminRegisteredEventHandler : INotificationHandler<AdminRegisteredEvent>
    {
        private readonly IMailManager _mailManager;
        private readonly ILogger<NewCustomerRegisteredEventHandler> _logger;

        public AdminRegisteredEventHandler(IMailManager mailManager, ILogger<NewCustomerRegisteredEventHandler> logger)
        {
            _mailManager = mailManager;
            _logger = logger;
        }

        public async Task Handle(AdminRegisteredEvent notification, CancellationToken cancellationToken)
        {
            //Call and send OTP here
            try
            {
                _mailManager.BuildMessageToAdminOnAdminRegistrationMessage(notification.Name, notification.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured sending email to Admin user");
            }
        }
    }
}
