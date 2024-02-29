using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Entities.DbEntities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime? WriteTime { get; set; }
        //public string? DoctorName { get; set; }
        //public string? PatientName { get; set; }

        //public int PatientId { get; set; }
        //public int DoctorId { get; set; }

        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }

    }
}
