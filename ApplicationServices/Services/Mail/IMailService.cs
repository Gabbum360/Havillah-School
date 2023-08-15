using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Services.Mail
{
    public interface IMailService
    {
        void BuildNewOtpNotificationMessage(string to, string adminName, string studentFullName, string studentEmail, string dateOfRegistration, string studentPhoneNumber);
    }
}
