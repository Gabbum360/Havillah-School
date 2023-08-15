using ApplicationServices.Common.Model;
using ApplicationServices.DataStorage;
using ApplicationServices.Features.Student.Commands;
using ApplicationServices.Features.Student.Dto;
using ApplicationServices.Interfaces.StudentInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Features.Student
{
    public class StudentLogic : IStudent
    {
        private readonly SMDatabaseContext _context;
        private readonly ILogger<StudentLogic> _logger;
        public StudentLogic(SMDatabaseContext context, ILogger<StudentLogic> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<StudentDto> Register(string firstname, string lastname, string sex, int age, string studentNo, string country)
        {
            var studentId = Guid.NewGuid();
            var newStudent = Entities.Student.Factory.Build(studentId, studentNo, firstname, lastname, sex, age, country);
            await _context.AddAsync(newStudent);
            await _context.SaveChangesAsync();
            var std = new StudentDto
            {
                StudentNo = studentNo,
                Firstname = firstname,
                Country = country
            };
            return std;
        }

        public async Task<bool> UpdateProfile(string studentNo)
        {
            var getStudent = await _context.Students.Where(s => s.StudentNo == studentNo).FirstOrDefaultAsync();
            var change = new StudentDto();
            change.Country = getStudent.Country;
            _context.Update(change);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<StudentDto>> GetStudents()
        {
            List<StudentDto> studentsList = new List<StudentDto>();
            var students = await _context.Students.ToListAsync();
            foreach (var item in students)
            {
                StudentDto std = new StudentDto();
                std.StudentNo = item.StudentNo;
                std.Sex = item.Sex;
                std.Age = item.Age;
                std.Country = item.Country;
                await _context.AddAsync(std);
            }
            return studentsList;
        }

        public async Task<StudentDto> GetStudent(Guid id)
        {
            var student = await _context.Students.Where(s => s.Id == id).FirstOrDefaultAsync();
            if(student == null)
            {
                throw new Exception("");
            }
            //map the student object to Dto instance
            var std = new StudentDto();
            std.StudentNo = student.StudentNo;
            std.Sex = student.Sex;
            std.Age = student.Age;
            std.Country = student.Country;
            await _context.AddAsync(std);
            return std;
        }

        public async Task<bool> RemoveStudent(Guid id)
        {
            var student = await _context.Students.Where(s => s.Id == id).Select(d => d).FirstOrDefaultAsync();
            _context.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }



    }
}
