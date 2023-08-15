using ApplicationServices.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.DataStorage
{
    public class SMDatabaseContext : DbContext
    {
        public SMDatabaseContext(DbContextOptions<SMDatabaseContext> options) : base(options)
        {

        }
        //public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }
        //public DbSet<Subject> Subjects { get; set; }
        //public DbSet<Staff> Staff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SMDatabaseContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
