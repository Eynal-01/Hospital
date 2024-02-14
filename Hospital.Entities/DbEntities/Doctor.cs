using Hospital.Core.Abstract;
using Hospital.Entities.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Entities.DbEntities
{
    public class Doctor : IdentityUser, IEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; } = "userWithoutPicture.png";
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Bio { get; set; }
        public string? DepartmentId { get; set; }
        public int ExperienceYear { get; set; }
        public string? Education { get; set; }
        public string? Status { get; set; }
        public DateTime WorkStartTime { get; set; }
        public DateTime WorkEndTime { get; set; }
        public ICollection<Patient>? Patients { get; set; }
        public ICollection<Recipe>? Recipes { get; set; }
    }
}
