using Hospital.Entities.DbEntities;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Entities.Data
{
    public class CustomIdentityDbContext : IdentityDbContext<CustomIdentityUser, CustomIdentityRole, string>
    {
        public CustomIdentityDbContext(DbContextOptions<CustomIdentityDbContext> option)
            : base(option)
        {
        }

        public DbSet<Appointment>? Appointments { get; set; }
        //public DbSet<Admin>? Admins { get; set; }
        public DbSet<Doctor>? Doctors { get; set; }
        //public DbSet<Notification>? Notifications { get; set; }
        //public DbSet<Attendance>? Attendances { get; set; }
        //public DbSet<Chat>? Chats { get; set; }
        //public DbSet<Message>? Messages { get; set; }
        //public DbSet<Calendar>? Calendar { get; set; }
        public DbSet<Department>? Departments { get; set; }
        //public DbSet<DoctorSchedule>? DoctorSchedules { get; set; }
        public DbSet<Patient>? Patients { get; set; }
        public DbSet<PatientAndDoctor>? PatientAndDoctors { get; set; }
        //public DbSet<Payment>? Payments { get; set; }
        //public DbSet<Recipe>? Recipes { get; set; }
        //public DbSet<Salary>? Salaries { get; set; }
    }
}
