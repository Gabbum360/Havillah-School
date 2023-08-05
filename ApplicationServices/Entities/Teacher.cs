using ApplicationServices.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Entities
{
    public class Teacher
    {
        private Teacher() { }

        public Teacher(Guid id, string firstname, string lastname, int age, string country, string staffNo)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            Age = age;
            Country = country;
            StaffNo = staffNo;
        }
        public Guid Id { get; private set; }
        public string? StaffNo { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string Sex { get; set; }
        public int Age { get; private set; }
        public string Country { get; private set; }
        public ClassCategory Class { get; private set; }
        public List<Class> ListOfClasses { get; } = new List<Class>();

        public Teacher SetFirstname(string name)
        {
            Firstname = name;
            return this;
        }
        public Teacher SetLastname(string name)
        {
            Lastname = name;
            return this;
        }
        public Teacher SetAge(int age)
        {
            Age = age;
            return this;
        }
        public Teacher SetStudentNo(int number)
        {
            var cfg = $"{Firstname}={number}";
            return this;
        }
        public Teacher SetCountry(string country)
        {
            Country = country;
            return this;
        }
        public class Factory
        {
            public static Teacher Build(Guid id, string studentNo, string firstname, string lastname, int age, string country)
            {
                return new Teacher(id, firstname, lastname, age, country, studentNo);
            }
            public static Teacher Build()
            {
                return new Teacher();
            }
        }
    }
}
