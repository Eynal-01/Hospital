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

        public CustomIdentityDbContext()
        {
        }

        public DbSet<Appointment>? Appointments { get; set; }
        public DbSet<Admin>? Admins { get; set; }
        public DbSet<Doctor>? Doctors { get; set; }
        public DbSet<Notification>? Notifications { get; set; }
        public DbSet<Attendance>? Attendances { get; set; }
        public DbSet<Chat>? Chats { get; set; }
        public DbSet<Message>? Messages { get; set; }
        public DbSet<Calendar>? Calendar { get; set; }
        public DbSet<Department>? Departments { get; set; }
        public DbSet<DoctorSchedule>? DoctorSchedules { get; set; }
        public DbSet<Patient>? Patients { get; set; }
        public DbSet<Payment>? Payments { get; set; }
        public DbSet<Recipe>? Recipes { get; set; }
        public DbSet<Salary>? Salaries { get; set; }
        public DbSet<AvailableTime>? AvailableTimes { get; set; }

        //public override int SaveChanges()
        //{
        //    SeedAvailableTimes();
        //    return base.SaveChanges();
        //}

        //private void SeedAvailableTimes()
        //{
        //    if (!Doctors.Any() && !AvailableTimes.Any())
        //    {
        //        var availableTimes = new List<AvailableTime>
        //    {
        //        new AvailableTime { StartTime = DateTime.Today.AddHours(9), EndTime = DateTime.Today.AddHours(9).AddMinutes(30)},
        //        new AvailableTime { StartTime = DateTime.Today.AddHours(9).AddMinutes(30), EndTime = DateTime.Today.AddHours(10)},
        //        new AvailableTime { StartTime = DateTime.Today.AddHours(10), EndTime = DateTime.Today.AddHours(10).AddMinutes(30)},
        //        new AvailableTime { StartTime = DateTime.Today.AddHours(10).AddMinutes(30), EndTime = DateTime.Today.AddHours(11)},
        //        new AvailableTime { StartTime = DateTime.Today.AddHours(11), EndTime = DateTime.Today.AddHours(11).AddMinutes(30)},
        //        new AvailableTime { StartTime = DateTime.Today.AddHours(11).AddMinutes(30), EndTime = DateTime.Today.AddHours(12)},

        //        new AvailableTime { StartTime = DateTime.Today.AddHours(13).AddMinutes(30), EndTime = DateTime.Today.AddHours(14)},
        //        new AvailableTime { StartTime = DateTime.Today.AddHours(14), EndTime = DateTime.Today.AddHours(14).AddMinutes(30)},
        //        new AvailableTime { StartTime = DateTime.Today.AddHours(14).AddMinutes(30), EndTime = DateTime.Today.AddHours(15)},
        //        new AvailableTime { StartTime = DateTime.Today.AddHours(15), EndTime = DateTime.Today.AddHours(15).AddMinutes(30)},
        //        new AvailableTime { StartTime = DateTime.Today.AddHours(15).AddMinutes(30), EndTime = DateTime.Today.AddHours(16)},
        //    };

        //        foreach (var doctor in Doctors)
        //        {
        //            doctor.AvailableTimes = availableTimes.Select(at => new AvailableTime
        //            {
        //                DoctorId = doctor.Id,
        //                StartTime = at.StartTime,
        //                EndTime = at.EndTime,
        //            }).ToList();
        //        }
        //        SaveChanges();
        //    }
        //}
    }
}