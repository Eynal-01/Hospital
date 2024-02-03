using Hospital.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HospitalProject.Entities.DbEntities
{
    public class Patient : CustomIdentityUser
    {
        public Guid PatientId { get; set; }
        public string? FullName { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public ICollection<Recipe>? Recipes { get; set; }
        public ICollection<Doctor>? Doctors { get; set; }
    }
}
