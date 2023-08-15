using ApplicationServices.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Features.Teacher.Dto
{
    public record AddTeacherDto
    {
        public Guid Id { get; set; }
        public string? StaffNo { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        //public ClassCategory? Teacherclass {get;set;}

        public static explicit operator AddTeacherDto(Entities.Teacher model)
        {
            return new AddTeacherDto()
            {

                Id = model.Id,
                StaffNo = model.StaffNo,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Sex = model.Sex,
                Age = model.Age,
                Country = model.Country,
            };
        }
    }
}
