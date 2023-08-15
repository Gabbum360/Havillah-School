using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Common.Options
{
    public class MailOption
    {
        public string BuildMessageToAdminOnStudentRegistrationMessage { get; set; }
        public string BuildMessageToAdminOnCustomerRegistrationMessage { get; set; } 
        public string From { get; set; }
        public string To { get; set; }
        public string APIKey { get; set; }
        public string NewStudentRegistration { get; set; }
    }
}
