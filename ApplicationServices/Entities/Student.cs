using ApplicationServices.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Entities
{
    public class Student
    {
        private Student() {}

        public Student(Guid id, string firstname, string lastname, int age, string country, string studentNo)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            Age = age;  
            Country = country;
            StudentNo = studentNo;
        }
        public Guid Id { get; private set; }
        public string? StudentNo { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string Sex { get; set; }
        public int Age { get; private set; }
        public string Country { get; private set; }
        //public List<Books> ListOfBooks { get; } = new List<Books>();
        public ClassCategory Class { get; private set; }  
        
        public Student SetFirstname(string name)
        {
            Firstname = name;
            return this;
        }
        public Student SetLastname(string name)
        {
            Lastname = name;
            return this;
        }
        public Student SetAge(int age)
        {
            Age = age;
            return this;
        }
        public Student SetStudentNo(int number)
        {
            var cfg = $"{Firstname}={number}";
            return this;
        }
        public Student SetCountry(string country)
        {
           Country = country;   
            return this;
        }
        public class Factory
        {
            public static Student Build(Guid id, string studentNo, string firstname, string lastname, int age, string country)
            {
                return new Student(id, firstname, lastname, age, country, studentNo);
            }
            public static Student Build()
            {
                return new Student();
            }
        }
    }
}
