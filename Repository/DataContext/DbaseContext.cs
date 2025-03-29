using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DataContext
{
    public class DbaseContext : DbContext
    {
        public DbaseContext(DbContextOptions<DbaseContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<StudentCourseRel>().HasKey(m => m.ID);
            //builder.Entity<StudentCourseRel>().OwnsOne(m => m.Course);
            //builder.Entity<StudentCourseRel>().OwnsOne(m => m.Student);

            builder.Entity<Student>().HasKey(m => m.ID);
            builder.Entity<Course>().HasKey(m => m.ID);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourseRel> StudentCourseRels { get; set; }
    }
}
