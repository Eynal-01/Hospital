using Hospital.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Entities.DbEntities
{
    public class Appointment:IEntity
    {
        public int Id { get; set; }
        public Patient? Patient { get; set; }
        public int Age { get; set; }
        public Doctor? Doctor { get; set; }
        public Department? Department { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public DateTime? AppointmentTime { get; set; }
        public string? Status { get; set; }
    }
}