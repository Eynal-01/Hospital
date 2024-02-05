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
<<<<<<< HEAD
=======
        //public Guid PatientId { get; set; }
>>>>>>> a3d3af79db5ec86d43d5266b80f27a3880416117
        public string? FullName { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public ICollection<Recipe>? Recipes { get; set; }
        public ICollection<Doctor>? Doctors { get; set; }
    }
}
