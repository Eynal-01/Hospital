using Hospital.Core.Abstract;
using Hospital.Entities.Data;
using Hospital.Entities.DbEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Entities.DbEntities
{
    public class Doctor : IdentityUser, IEntity, CommonUsers
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; } = "https://res.cloudinary.com/dvcq24ogl/image/upload/v1709901715/oc7rwjbfbbgjq5e2gqso.png";
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Bio { get; set; }
        public string? DepartmentId { get; set; }
        public int? ScheduleId { get; set; }
        public virtual Schedule?Schedule { get; set; }
        public int? RoomId { get; set; }
        public virtual Room? Room { get; set; }
        public int ExperienceYear { get; set; }
        public string? Education { get; set; }
        public string? Status { get; set; }
        public bool? IsPostView { get; set; }
        public int MissedNotifCount { get; set; }

        public DateTime WorkStartTime { get; set; }
        public DateTime WorkEndTime { get; set; }
        public ICollection<Patient>? Patients { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
        public ICollection<Recipe>? Recipes { get; set; }
        //public virtual Department? Department { get; set; }
        public int WorkDayCount { get; set; }
        public virtual Department? Department { get; set; }
        public virtual ICollection<PostView>? PostViews { get; set; }
        public virtual ICollection<Chat>? Chats { get; set; }

        //public ICollection<AvailableTime>? AvailableTimes { get; set; }
    }
}
