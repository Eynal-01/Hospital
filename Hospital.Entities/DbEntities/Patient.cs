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
    public class Patient : IdentityUser
    {
        public Guid PatientId { get; set; }
        public string? FullName { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
<<<<<<< HEAD
        //public ICollection<string>? Recipes { get; set; }
        //public ICollection<Doctor> Doctors { get; set; }
=======
        public ICollection<Recipe>? Recipes { get; set; }
        public ICollection<Doctor>? Doctors { get; set; }
>>>>>>> 2a999889b507aa5389929ff9d5c2622f59f4ec94
    }
}
