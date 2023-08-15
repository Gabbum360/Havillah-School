using ApplicationServices.Features.Student.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Interfaces.StudentInterfaces
{
    public interface IStudent
    {
        Task<StudentDto> Register(string firstname, string lastname, string sex, int age, string studentNo, string country);
        Task<bool> UpdateProfile(string studentNo);
        Task<List<StudentDto>> GetStudents();
        Task<StudentDto> GetStudent(Guid id);
        Task<bool> RemoveStudent(Guid id);
    }
}
