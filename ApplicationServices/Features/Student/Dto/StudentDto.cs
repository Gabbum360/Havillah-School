using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Features.Student.Dto
{
    public record StudentDto
    {
        public string? StudentNo { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }

        public static explicit operator StudentDto(Entities.Student std)
        {
            return new StudentDto()
            {
                Firstname = std.Firstname,
                Lastname = std.Lastname,
                Sex = std.Sex,
                Age = std.Age,
                Country = std.Country,
                StudentNo = std.StudentNo,
            };
        }
    }
}
