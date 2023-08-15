using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Features.Student.Commands
{
    public class RegisterStudentCommand /*: IRequest<>*/
    {
        public string? StudentNo { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
    }
}
