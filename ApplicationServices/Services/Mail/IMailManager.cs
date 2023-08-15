using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Services.Mail
{
    public interface IMailManager
    {
        MailManager BuildMessageToAdminOnStudentRegistrationMessage(string adminEmail, string studentFullName, string studentEmail, string studentPhoneNumber, string dateOfRegistration);
        MailManager BuildMessageToAdminOnAdminRegistrationMessage(string name, string email);
    }
}
