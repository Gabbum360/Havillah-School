using ApplicationServices.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.DataStorage
{
    public class SMDatabaseContext : DbContext
    {
        public SMDatabaseContext(DbContextOptions<SMDatabaseContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }
        //public DbSet<Subject> Subjects { get; set; }
        //public DbSet<Staff> Staff { get; set; }
    }
}
