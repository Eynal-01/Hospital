using Hospital.Core.Abstract;
using Hospital.Entities.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HospitalProject.Entities.DbEntities
{
    public class Patient : CustomIdentityUser, IEntity
    {
        public string? FullName { get; set; }
        public int Age { get; set; }
        public string? Avatar { get; set; } = "userWithoutPicture.png";
        public string? Address { get; set; }
        public ICollection<Recipe>? Recipes { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
        //public virtual ICollection<Doctor>? Doctors { get; set; }
    }
}
