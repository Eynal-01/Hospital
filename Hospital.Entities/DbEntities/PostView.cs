using Hospital.Entities.Data;
using HospitalProject.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Entities.DbEntities
{
    public class PostView
    {
        public int Id { get; set; }
        public string? AdminId { get; set; }
        public string? DoctorId { get; set; }
        public string? PatientId { get; set; }

        public int PostId { get; set; }

        //public CommonUsers? User { get; set; }
        public Admin? Admin { get; set; }
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
        public Post? Post { get; set; }
    }
}
