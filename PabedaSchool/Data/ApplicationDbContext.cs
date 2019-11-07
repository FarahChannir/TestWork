using Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PabedaSchool.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            base.OnModelCreating(builder);

        }
    public DbSet<School> SchoolSet { get; set; }
    public DbSet<Teacher> TeacherSet { get; set; }
    public DbSet<Student> StudentSet { get; set; }
    public DbSet<LogSystem> LogSystemSet { get; set; }
    public DbSet<TeacherSchool> TeacherSchoolSet { get; set; }
    public DbSet<ClassSchool> ClassSchoolSet { get; set; }
    }
}
